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

		DashboardViewController dashboardViewController;
		public LoginViewController () : base ("LoginViewController", null)
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
		}


		public void connectionStateChanged(NSNotification notification){

			SHKNotification showNotice;
			NSString value;

			showNotice = (SHKNotification)notification.Object;

			value = (NSString)showNotice.Value;


			if (value == ShowKit.Constants.SHKConnectionStatusLoggedIn) {


				this.dashboardViewController = new DashboardViewController ();
				this.NavigationController.PushViewController (this.dashboardViewController, true);
				Console.WriteLine("shk status changed "+value);

			}

		}


		partial void loginAction (NSObject sender)
		{
			if (this.usernameTextField.Text.Length > 0 && this.passwordTextField.Text.Length > 0)
			{
				string username = Constants.PREFIX+this.usernameTextField.Text;
				string password = this.passwordTextField.Text;
				ShowKit.ShowKit.Login(username,password);
				Console.WriteLine("hello1");

			}
		}

		partial void parseLoginAction (NSObject sender)
		{
			Console.WriteLine("hello2");
		}
	}
}

