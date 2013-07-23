// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace ShowKitApp
{
	[Register ("LoginViewController")]
	partial class LoginViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITextField passwordTextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField usernameTextField { get; set; }

		[Action ("loginAction:")]
		partial void loginAction (MonoTouch.Foundation.NSObject sender);

		[Action ("parseLoginAction:")]
		partial void parseLoginAction (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (usernameTextField != null) {
				usernameTextField.Dispose ();
				usernameTextField = null;
			}

			if (passwordTextField != null) {
				passwordTextField.Dispose ();
				passwordTextField = null;
			}
		}
	}
}
