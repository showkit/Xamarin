using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ShowKit;

namespace ShowKitApp
{
	public partial class RegisterViewController : UIViewController
	{
		NSObject observer;

		DashboardViewController dashboardViewController;

		public RegisterViewController () : base ("RegisterViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.observer = NSNotificationCenter.DefaultCenter.AddObserver (ShowKit.Constants.SHKConnectionStatusChangedNotification, connectionStateChanged);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			if (observer != null) {
				NSNotificationCenter.DefaultCenter.RemoveObserver (observer);
				observer = null;
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		partial void parseRegisterAction (NSObject sender)
		{
//			throw new System.NotImplementedException ();
		}

		partial void registerAction (NSObject sender)
		{
			if(this.usernameTextField.Text.Length > 0 && this.passwordTextField.Text.Length > 0)
			{
				string uname = this.usernameTextField.Text;
				string password = this.passwordTextField.Text;
				ShowKit.ShowKit.RegisterUser(uname,password, Constants.APIKEY, delegate (NSString username, NSError error){
					if (error == null){
						ShowKit.ShowKit.Login(username, password);
					}else{
						UIAlertView alert = new UIAlertView("Error", error.LocalizedDescription, null, "OK");
						alert.Show ();
					}
				});
			}
		}

		public void connectionStateChanged(NSNotification notification){

			SHKNotification showNotice;
			NSString value;

			showNotice = (SHKNotification)notification.Object;

			value = (NSString)showNotice.Value;


			if (value == ShowKit.Constants.SHKConnectionStatusLoggedIn) {
				this.dashboardViewController = new DashboardViewController ();
				this.NavigationController.PushViewController (this.dashboardViewController, true);
			}

		}
	}
}

