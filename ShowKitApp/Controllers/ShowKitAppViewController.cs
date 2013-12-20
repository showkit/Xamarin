using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ShowKit;

namespace ShowKitApp
{
	public partial class ShowKitAppViewController : UIViewController
	{

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public ShowKitAppViewController ()
			: base (UserInterfaceIdiomIsPhone ? "ShowKitAppViewController_iPhone" : "ShowKitAppViewController_iPad", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			NSUserDefaults user = NSUserDefaults.StandardUserDefaults;
			String username = user.StringForKey ("username");
			if (username != null) {
				this.loginAction (this);
			} else {
				this.registerAction (this);
			}
		}

		partial void loginAction (NSObject sender)
		{
			this.NavigationController.PushViewController(new LoginViewController(), true);
		}

		partial void registerAction (NSObject sender)
		{
			this.NavigationController.PushViewController(new RegisterViewController(), true);
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			if (UserInterfaceIdiomIsPhone) {
				return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
			} else {
				return true;
			}
		}


	}
}