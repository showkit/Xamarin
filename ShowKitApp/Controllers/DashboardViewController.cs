using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ShowKit;
using ParseTouch;

namespace ShowKitApp
{
	public partial class DashboardViewController : UIViewController
	{
		NSObject connectionStatusObserver;
		NSObject messageObserver;
		NSObject remoteClientStatusObserver;

		UIAlertViewDelegate alertDelegate;
		UIAlertViewDelegate alertGestureDelegate;
		DashboardViewController dashboardViewController;
		ShareScreenViewController shareScreenViewController;

		GestureDemoViewController gestureDemoViewController;

		ScreenDemoViewController screenDemoViewController;
		public DashboardViewController () : base ("DashboardViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();			
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			ShowKit.ShowKit.SetState (this.mainVideoUIView, ShowKit.Constants.SHKMainDisplayViewKey);
			ShowKit.ShowKit.SetState (this.prevVideoUIView, ShowKit.Constants.SHKPreviewDisplayViewKey);
	
			this.connectionStatusObserver = NSNotificationCenter.DefaultCenter.AddObserver (ShowKit.Constants.SHKConnectionStatusChangedNotification, connectionStateChanged);
			this.messageObserver = NSNotificationCenter.DefaultCenter.AddObserver (ShowKit.Constants.SHKUserMessageReceivedNotification, userMessageReceived);
			this.remoteClientStatusObserver = NSNotificationCenter.DefaultCenter.AddObserver (ShowKit.Constants.SHKRemoteClientStateChangedNotification, remoteClientStatusChanged);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

//			ShowKit.ShowKit.SetState (null, "SHKMainDisplayViewKey");
//			ShowKit.ShowKit.SetState (null, "SHKPreviewDisplayViewKey");
//			ShowKit.ShowKit.SetState ("SHKVideoLocalPreviewDisabled", "SHKVideoLocalPreviewModeKey");
			if (connectionStatusObserver != null) {
				NSNotificationCenter.DefaultCenter.RemoveObserver (connectionStatusObserver);
				connectionStatusObserver = null;
			}

			if (messageObserver != null) {
				NSNotificationCenter.DefaultCenter.RemoveObserver (messageObserver);
				messageObserver = null;
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.NavigationItem.HidesBackButton = true;			

			this.NavigationItem.SetRightBarButtonItem(
				new UIBarButtonItem(UIBarButtonSystemItem.Action, (sender,args) => {

				NSString state = (NSString)ShowKit.ShowKit.GetStateForKey(ShowKit.Constants.SHKConnectionStatusKey);

				switch (state)
				{
				case ShowKit.Constants.SHKConnectionStatusLoggedIn:
					var parseUser = ParseUser.CurrentUser();
					if(parseUser == null)
						ShowKit.ShowKit.Logout();
					else
						ShowKit.SHKParseUser.LogOut();
					break;
				case ShowKit.Constants.SHKConnectionStatusInCall:
					ShowKit.ShowKit.HangupCall();
					break;
				default:
					this.NavigationController.PopViewControllerAnimated (true);
					Console.WriteLine("default here");
					break;
				}
			})
				, true);
			this.dashboardViewController = this;
			this.alertDelegate = new AlertDelegate (this);
			this.alertGestureDelegate = new AlertGestureDelegate (this);
		}

		public void connectionStateChanged(NSNotification notification){

			SHKNotification showNotice;
			NSString value;

			showNotice = (SHKNotification)notification.Object;

			value = (NSString)showNotice.Value;

			switch (value) 
			{
				case ShowKit.Constants.SHKConnectionStatusCallTerminated:
					var parseUser = ParseUser.CurrentUser();
					if(parseUser == null)
						ShowKit.ShowKit.Logout();
					else
						ShowKit.SHKParseUser.LogOut();
					break;
				case ShowKit.Constants.SHKConnectionStatusInCall:
					setLeftBarButton ();
					break;
				case ShowKit.Constants.SHKConnectionStatusLoggedIn:
					break;
				case ShowKit.Constants.SHKConnectionStatusNotConnected:
					this.NavigationController.PopViewControllerAnimated (true);
					break;
				case ShowKit.Constants.SHKConnectionStatusLoginFailed:
					break;
				case ShowKit.Constants.SHKConnectionStatusCallIncoming:
					Console.Write ("callee: " + (NSString)showNotice.UserObject);
					handleBackgroundCalling ((NSString)showNotice.UserObject);
					handleCallIncoming ();
					break;
			case ShowKit.Constants.SHKConnectionStatusCallFailed:
					NSError error = (NSError)showNotice.UserObject;
					this.callContainer.Hidden = false;
					UIAlertView alert = new UIAlertView("Error", error.LocalizedDescription+" Please try calling again.", null, "OK");
					alert.Show ();
					break;
			}
		}

		public void handleBackgroundCalling(String caller)
		{
			if(UIApplication.SharedApplication.ApplicationState == UIApplicationState.Background)
			{
				UILocalNotification ln = new UILocalNotification ();
				ln.FireDate = DateTime.Now;
				ln.AlertBody = "Incoming call from " + caller;
				ln.SoundName = UILocalNotification.DefaultSoundName;
				UIApplication.SharedApplication.ScheduleLocalNotification(ln);
			}
		}
		public void userMessageReceived(NSNotification notification){

			SHKNotification showNotice;
			NSData data;
			NSString value;
			showNotice = (SHKNotification)notification.Object;

			data = (NSData)showNotice.Value;

			value = getMessage (data);

			switch (value) 
			{
				case "request share gesture":
					InvokeOnMainThread (delegate { 
						UIAlertView alert = new UIAlertView ("Gesture Alert", "Someone wants to initiate gesture control with you. Would you like to accept it?", alertGestureDelegate, "Accept", "Reject");
						alert.Show ();
					});
					break;
				case "accept share gesture":
					InvokeOnMainThread (delegate { 
						dashboardViewController.shareGestureButton.Hidden = false;
					});
				ShowKit.ShowKit.SetState((NSString)ShowKit.Constants.SHKGestureCaptureLocalIndicatorsOn, ShowKit.Constants.SHKGestureCaptureLocalIndicatorsModeKey);
					Console.Write ("accept share gesture received");
					break;
				case "reject share gesture":	
					break;	
				case "terminate share gesture":
					break;
				case "request share screen":
					InvokeOnMainThread (delegate { 
						this.prevVideoUIView.Hidden = true;
						this.NavigationItem.LeftBarButtonItem = null;			
						this.NavigationItem.HidesBackButton = true;
						this.bottomToolBar.Hidden = true;
					});
					ShowKit.ShowKit.SetState ((NSString)ShowKit.Constants.SHKVideoScaleModeFit, ShowKit.Constants.SHKVideoScaleModeKey);
					break;	
				case "request conference":
					setLeftBarButton ();
					break;
				case "whatsup":
					InvokeOnMainThread (delegate { 					
						UIAlertView alert = new UIAlertView("Alert", "Someone send you an whatsup message", null, "OK");
						alert.Show ();
					});
					break;			
			}
		}

		public void remoteClientStatusChanged(NSNotification notification){

			SHKNotification showNotice;
			NSString value;

			showNotice = (SHKNotification)notification.Object;

			value = (NSString)showNotice.Value;

			switch (value) 
			{
				case ShowKit.Constants.SHKRemoteClientVideoStateStarted:
					break;
				case ShowKit.Constants.SHKRemoteClientVideoStateStopped:
					break;
				case ShowKit.Constants.SHKRemoteClientAudioStateStarted:
					break;
				case ShowKit.Constants.SHKRemoteClientAudioStateStopped:
					break;
				case ShowKit.Constants.SHKRemoteClientGestureStateStarted:
				ShowKit.ShowKit.SetState((NSString)ShowKit.Constants.SHKGestureCaptureModeReceive, ShowKit.Constants.SHKGestureCaptureModeKey);
					break;
				case ShowKit.Constants.SHKRemoteClientGestureStateStopped:
				ShowKit.ShowKit.SetState((NSString)ShowKit.Constants.SHKGestureCaptureModeOff, ShowKit.Constants.SHKGestureCaptureModeKey);
					break;
			}
		}

		public void setLeftBarButton(){
			InvokeOnMainThread (delegate { 
				this.prevVideoUIView.Hidden = false;
				this.NavigationItem.HidesBackButton = false;
				this.bottomToolBar.Hidden = false;
				ShowKit.ShowKit.SetState ((NSString)ShowKit.Constants.SHKVideoScaleModeFill, ShowKit.Constants.SHKVideoScaleModeKey);
				Console.WriteLine("coming through here right now");
				this.NavigationItem.SetLeftBarButtonItem(
					new UIBarButtonItem("share screen", UIBarButtonItemStyle.Plain, (sender,args) => {
					NSString state = (NSString)ShowKit.ShowKit.GetStateForKey(ShowKit.Constants.SHKVideoInputDeviceKey);
					NSString SHKVideoInputDeviceScreen = new NSString(ShowKit.Constants.SHKVideoInputDeviceScreen);
					NSString SHKVideoInputDeviceFrontCamera = new NSString(ShowKit.Constants.SHKVideoInputDeviceFrontCamera);
					if (state == ShowKit.Constants.SHKVideoInputDeviceScreen)
					{
						ShowKit.ShowKit.SetState ((NSString)ShowKit.Constants.SHKVideoScaleModeFill, ShowKit.Constants.SHKVideoScaleModeKey);

						this.mainVideoUIView.Hidden = false;
						this.prevVideoUIView.Hidden = false;
						this.shareContainer.Hidden = true;
						this.sendButton.Hidden = true;
						this.NavigationItem.LeftBarButtonItem.Title = "share screen";
						ShowKit.ShowKit.SetState(SHKVideoInputDeviceFrontCamera, ShowKit.Constants.SHKVideoInputDeviceKey);
						this.sendMessage(new NSString("request conference"));
						this.bottomToolBar.Hidden = false;
					}else
					{
						this.mainVideoUIView.Hidden = true;
						this.prevVideoUIView.Hidden = true;
						this.shareContainer.Hidden = false;
						this.sendButton.Hidden = false;
						this.NavigationItem.LeftBarButtonItem.Title = "conference";
						ShowKit.ShowKit.SetState(SHKVideoInputDeviceScreen, ShowKit.Constants.SHKVideoInputDeviceKey);
						dashboardViewController.sendMessage(new NSString("request share screen"));
						this.bottomToolBar.Hidden = true;
					}
					if (this.usernameTextField.IsFirstResponder){
						this.usernameTextField.ResignFirstResponder();
					}
				}), true);
			});
		}

		partial void makeCallAction (NSObject sender)
		{
			if (this.usernameTextField.IsFirstResponder){
				this.usernameTextField.ResignFirstResponder();
			}
			if (this.usernameTextField.Text.Length > 0)
			{
				dashboardViewController.callContainer.Hidden = true;
				ParseQuery query = ParseUser.Query();
				query.WhereKey("username", (NSString)this.usernameTextField.Text);
				ParseObject[] userObjects = query.FindObjects ();
				if(userObjects.Length > 0){
					var user = (ParseUser)userObjects[0];
					ShowKit.SHKParseUser.InitiateCallWithPFUser(user);
				}else{
					ShowKit.ShowKit.InitiateCallWithUser(Constants.PREFIX + this.usernameTextField.Text);
				}
			}
		}

		public NSString getMessage(NSData data )
		{
			NSKeyedUnarchiver unarchiver = new NSKeyedUnarchiver(data);
			NSDictionary dictionary = (NSDictionary)unarchiver.DecodeObject ();
			NSString value = (NSString)dictionary.ObjectForKey (new NSString ("msg"));
			return value;
		}

		public void sendMessage(NSString msg)
		{
			NSDictionary message = NSDictionary.FromObjectAndKey(msg, new NSString("msg"));
			NSMutableData data = new NSMutableData ();
			NSKeyedArchiver archiver = new NSKeyedArchiver (data);
			archiver.Encode (message);
			archiver.FinishEncoding ();
			ShowKit.ShowKit.SendMessage (data);
		}

		partial void shareScreenAction (NSObject sender)
		{
			this.screenDemoViewController = new ScreenDemoViewController();
			this.NavigationController.PushViewController(this.screenDemoViewController, true);		
		}

		partial void shareGestureAction (NSObject sender)
		{

			this.gestureDemoViewController = new GestureDemoViewController();
			this.NavigationController.PushViewController(this.gestureDemoViewController, true);
		}

		partial void shareGestureSwitch (NSObject sender)
		{
			if(shareGestureOutlet.On){
				sendMessage(new NSString(@"request share gesture"));
			}else{
				ShowKit.ShowKit.SetState((NSString)ShowKit.Constants.SHKGestureCaptureLocalIndicatorsOff, ShowKit.Constants.SHKGestureCaptureLocalIndicatorsModeKey);

				ShowKit.ShowKit.SetState((NSString)ShowKit.Constants.SHKGestureCaptureModeOff, ShowKit.Constants.SHKGestureCaptureModeKey);
				dashboardViewController.shareGestureButton.Hidden = true;
			}
		}

		partial void sendAction (NSObject sender)
		{
			sendMessage(new NSString("whatsup"));
		}

		partial void framebufferAction (NSObject sender)
		{
			UIBarButtonItem framebufferButton = (UIBarButtonItem)sender;
			switch(framebufferButton.Title){
				case "framebuffer":
					modifyFrameBuffer("<<");
					framebufferButton.Title = "+framebuffer";
					break;
				case "+framebuffer":
					modifyFrameBuffer(">>");	
					framebufferButton.Title = "-framebuffer";
					break;
				case "-framebuffer":
					modifyFrameBuffer("");
					framebufferButton.Title = "framebuffer";
					break;
			}
		}

		public unsafe void modifyFrameBuffer(String type)
		{
			ShowKit.ShowKit.SetVideoBufferCallBack (delegate (IntPtr data, int width, int height, PixelFormat pixelFormat, bool isEncoding) {
				if(type != ""){
					int length = 0;

					switch (pixelFormat)
					{
						case PixelFormat.BGRA32:
						length = width * height * 4;
						break;
						case PixelFormat.NV12:
						length = (width * height * 3)/2;

						break;
						case PixelFormat.YUV420:
						length = (width * height * 3)/2;
						break;
					}

					byte* ptr = (byte*)data;

					if (isEncoding){
						for (int i = 0; i < length; i++) {
							if (type == "<<")
								*(ptr + i) = (byte)(*(ptr + i) << 2);
							else if (type == ">>")
								*(ptr + i) = (byte)(*(ptr + i) >> 2);
						}
					}
				}
			});
		}

		public void logoutAction ()
		{

		}

		public void handleCallIncoming ()
		{
			dashboardViewController.callContainer.Hidden = true;
			UIAlertView alert = new UIAlertView("Call Incoming", "You have a call. Would you like to accept it?", alertDelegate, "Accept", "Reject");
			alert.Show ();
		}

		public class AlertDelegate : UIAlertViewDelegate 
		{
			DashboardViewController dashboardViewController;
			public AlertDelegate(DashboardViewController viewController)
			{
				this.dashboardViewController = viewController;
			}

			public override void Clicked (UIAlertView alertView,int index)
			{
				if (index == 0) 
				{
					dashboardViewController.NavigationItem.SetLeftBarButtonItem(
						new UIBarButtonItem("share screen", UIBarButtonItemStyle.Plain, (sender,args) => {
						NSString state = (NSString)ShowKit.ShowKit.GetStateForKey(ShowKit.Constants.SHKVideoInputDeviceKey);
						NSString SHKVideoInputDeviceScreen = new NSString(ShowKit.Constants.SHKVideoInputDeviceScreen);
						NSString SHKVideoInputDeviceFrontCamera = new NSString(ShowKit.Constants.SHKVideoInputDeviceFrontCamera);
						if (state == ShowKit.Constants.SHKVideoInputDeviceScreen)
						{
							ShowKit.ShowKit.SetState ((NSString)ShowKit.Constants.SHKVideoScaleModeFill, ShowKit.Constants.SHKVideoScaleModeKey);
							dashboardViewController.mainVideoUIView.Hidden = false;
							dashboardViewController.prevVideoUIView.Hidden = false;
							dashboardViewController.shareContainer.Hidden = true;						
							dashboardViewController.sendButton.Hidden = true;
							dashboardViewController.NavigationItem.LeftBarButtonItem.Title = "share screen";
							ShowKit.ShowKit.SetState(SHKVideoInputDeviceFrontCamera, ShowKit.Constants.SHKVideoInputDeviceKey);
							dashboardViewController.sendMessage(new NSString("request conference"));
							dashboardViewController.bottomToolBar.Hidden = false;
						}else
						{
							dashboardViewController.mainVideoUIView.Hidden = true;
							dashboardViewController.prevVideoUIView.Hidden = true;
							dashboardViewController.shareContainer.Hidden = false;
							dashboardViewController.sendButton.Hidden = false;
							dashboardViewController.NavigationItem.LeftBarButtonItem.Title = "conference";
							ShowKit.ShowKit.SetState(SHKVideoInputDeviceScreen, ShowKit.Constants.SHKVideoInputDeviceKey);
							dashboardViewController.sendMessage(new NSString("request share screen"));
							dashboardViewController.bottomToolBar.Hidden = true;
						}

						if (dashboardViewController.usernameTextField.IsFirstResponder){
							dashboardViewController.usernameTextField.ResignFirstResponder();
						}
					})
						, true);
					ShowKit.ShowKit.AcceptCall ();
				} else {
					dashboardViewController.callContainer.Hidden = false;
					ShowKit.ShowKit.RejectCall ();
				}
			}
		}

		public class AlertGestureDelegate : UIAlertViewDelegate 
		{
			DashboardViewController dashboardViewController;
			public AlertGestureDelegate(DashboardViewController viewController)
			{
				this.dashboardViewController = viewController;
			}
			public override void Clicked (UIAlertView alertView,int index)
			{
				if (index == 0) 
				{
					dashboardViewController.sendMessage (new NSString("accept share gesture"));
					ShowKit.ShowKit.SetState ((NSString)ShowKit.Constants.SHKGestureCaptureModeBroadcast, ShowKit.Constants.SHKGestureCaptureModeKey);
				} else {

				}
			}
		}
	}
}