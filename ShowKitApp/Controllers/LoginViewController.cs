using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ShowKit;

namespace ShowKitApp
{
	public partial class LoginViewController : UIViewController
	{
		NSObject observer;

		public LoginViewController () : base ("LoginViewController", null) {}

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
			NSUserDefaults user = NSUserDefaults.StandardUserDefaults;
			String username = user.StringForKey ("username");
			if (username != null)
				this.usernameTextField.Text = username;
		}


		public void connectionStateChanged(NSNotification notification){

			SHKNotification showNotice;
			NSString value;

			showNotice = (SHKNotification)notification.Object;

			value = (NSString)showNotice.Value;


			if (value == ShowKit.Constants.SHKConnectionStatusLoggedIn) {
				this.NavigationController.PushViewController (new DashboardViewController (), true);
				Console.WriteLine("shk status changed "+value);

			}

		}


		partial void loginAction (NSObject sender)
		{
			if (this.usernameTextField.Text != null && this.passwordTextField.Text != null)
			{
				NSUserDefaults user = NSUserDefaults.StandardUserDefaults;
				string username = Constants.PREFIX+this.usernameTextField.Text;
				user.SetString(this.usernameTextField.Text, "username");
				string password = this.passwordTextField.Text;
				ShowKit.ShowKit.Login(username,password);

			}
		}
	}
}
