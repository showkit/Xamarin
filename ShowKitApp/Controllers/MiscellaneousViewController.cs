using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;

namespace ShowKitApp
{
	public partial class MiscellaneousViewController : UIViewController
	{

		public MiscellaneousViewController () : base ("MiscellaneousViewController", null)
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
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
		partial void actionSheetAction (NSObject sender)
		{
			UIActionSheet actionSheet = new UIActionSheet ("Action Sheet Test");
			actionSheet.AddButton ("Red");
			actionSheet.AddButton ("Blue");
			actionSheet.AddButton ("Green");
			actionSheet.AddButton ("White");
			actionSheet.Clicked += delegate(object a, UIButtonEventArgs b) {

				switch(b.ButtonIndex){
				case 0:
					this.View.BackgroundColor = UIColor.Red;
					break;
				case 1:
					this.View.BackgroundColor = UIColor.Blue;
					break;
				case 2:
					this.View.BackgroundColor = UIColor.Green;
					break;
				case 3:
					this.View.BackgroundColor = UIColor.White;
					break;
				}

			};
			actionSheet.ShowInView (View);
		
		
		}

		partial void menuAction (NSObject sender)
		{
			this.View.BecomeFirstResponder();
			var menuController = UIMenuController.SharedMenuController;

			var red = new UIMenuItem ("Red", new Selector ("redAction"));
			var blue = new UIMenuItem ("Blue", new Selector ("blueAction"));
			var green = new UIMenuItem ("Green", new Selector ("greenAction"));

			UIMenuController menu = UIMenuController.SharedMenuController;
			var bounds = new RectangleF (this.menuButton.Frame.X, this.menuButton.Frame.Y, 320, 110);

			menu.SetTargetRect(bounds,this.View);
			menu.MenuItems = new [] {red, blue, green};

			menu.MenuVisible = true;
		}


		[Export("redAction")]
		void redAction ()
		{

		}

		[Export("blueAction")]
		void blueAction ()
		{

		}

		[Export("greenAction")]
		void greenAction ()
		{

		}

	}
}

