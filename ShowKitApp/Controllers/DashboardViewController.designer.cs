// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace ShowKitApp
{
	[Register ("DashboardViewController")]
	partial class DashboardViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIToolbar bottomToolBar { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIScrollView callContainer { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView mainVideoUIView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView prevVideoUIView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton sendButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIScrollView shareContainer { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton shareGestureButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch shareGestureOutlet { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField usernameTextField { get; set; }

		[Action ("framebufferAction:")]
		partial void framebufferAction (MonoTouch.Foundation.NSObject sender);

		[Action ("makeCallAction:")]
		partial void makeCallAction (MonoTouch.Foundation.NSObject sender);

		[Action ("sendAction:")]
		partial void sendAction (MonoTouch.Foundation.NSObject sender);

		[Action ("shareGestureAction:")]
		partial void shareGestureAction (MonoTouch.Foundation.NSObject sender);

		[Action ("shareGestureSwitch:")]
		partial void shareGestureSwitch (MonoTouch.Foundation.NSObject sender);

		[Action ("shareScreenAction:")]
		partial void shareScreenAction (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (callContainer != null) {
				callContainer.Dispose ();
				callContainer = null;
			}

			if (mainVideoUIView != null) {
				mainVideoUIView.Dispose ();
				mainVideoUIView = null;
			}

			if (prevVideoUIView != null) {
				prevVideoUIView.Dispose ();
				prevVideoUIView = null;
			}

			if (sendButton != null) {
				sendButton.Dispose ();
				sendButton = null;
			}

			if (shareContainer != null) {
				shareContainer.Dispose ();
				shareContainer = null;
			}

			if (shareGestureButton != null) {
				shareGestureButton.Dispose ();
				shareGestureButton = null;
			}

			if (shareGestureOutlet != null) {
				shareGestureOutlet.Dispose ();
				shareGestureOutlet = null;
			}

			if (usernameTextField != null) {
				usernameTextField.Dispose ();
				usernameTextField = null;
			}

			if (bottomToolBar != null) {
				bottomToolBar.Dispose ();
				bottomToolBar = null;
			}
		}
	}
}
