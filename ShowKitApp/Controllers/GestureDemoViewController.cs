using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using OpenGLES20Example;

namespace ShowKitApp
{
	public partial class GestureDemoViewController : UIViewController
	{
		MiscellaneousViewController miscellaneousViewController;
		HiddenTextFieldViewController hiddenTextFieldViewController;
		GLViewController glViewController;


		public GestureDemoViewController () : base ("GestureDemoViewController", null)
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

		partial void redAction (NSObject sender)
		{
			this.View.BackgroundColor = UIColor.Red;
		}

		partial void blueAction (NSObject sender)
		{
			this.View.BackgroundColor = UIColor.Blue;
		}

		partial void greenAction (NSObject sender)
		{
			this.View.BackgroundColor = UIColor.Green;
		}

		partial void whiteAction (NSObject sender)
		{
			this.View.BackgroundColor = UIColor.White;
		}

		partial void alertAction (NSObject sender)
		{
			InvokeOnMainThread (delegate { 
				UIAlertView alert = new UIAlertView("Alert", "You have click alert !^_^!", null, "OK");
				alert.Show ();
			});
		}

		partial void segmentedControlAction (NSObject sender)
		{
			UISegmentedControl segmentControl = (UISegmentedControl)sender;
			switch(segmentControl.SelectedSegment){
			case 0:
				this.labelView.Text = "First";
				break;
			case 1:
				this.labelView.Text = "Second";
				break;
			case 2:
				this.labelView.Text = "Third";
				break;
			}
		}

		partial void sliderAction (NSObject sender)
		{
			UISlider slider = (UISlider)sender;
			this.labelView.Text = slider.Value.ToString();
		}

		partial void stepperAction (NSObject sender)
		{
			UIStepper stepper = (UIStepper)sender;
			this.labelView.Text = stepper.Value.ToString();
		}

		partial void switchAction (NSObject sender)
		{
			UISwitch mswitch = (UISwitch)sender;
			if(mswitch.On){
				this.labelView.Text = "on";
			}else{
			}	this.labelView.Text = "off";
		}

		partial void miscellaneousAction (NSObject sender)
		{

			this.miscellaneousViewController = new MiscellaneousViewController();
			this.NavigationController.PushViewController(this.miscellaneousViewController, true);
		}

		partial void hiddenAction (NSObject sender)
		{
			this.hiddenTextFieldViewController = new HiddenTextFieldViewController();
			this.NavigationController.PushViewController(this.hiddenTextFieldViewController, true);
		}

		partial void openglAction (NSObject sender)
		{
			this.glViewController = new GLViewController();
			this.NavigationController.PushViewController(this.glViewController, true);
			(this.glViewController.View as GLView).StartAnimation ();
		}

	}
}

