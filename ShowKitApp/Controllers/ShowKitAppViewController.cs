using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ShowKit;

namespace ShowKitApp
{
	public partial class ShowKitAppViewController : UIViewController
	{

		LoginViewController loginViewController;
		RegisterViewController registerViewController;

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
		}

		partial void loginAction (NSObject sender)
		{

			this.loginViewController = new LoginViewController();
			this.NavigationController.PushViewController(this.loginViewController, true);
		}

		partial void registerAction (NSObject sender)
		{
			this.registerViewController = new RegisterViewController();
			this.NavigationController.PushViewController(this.registerViewController, true);
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

