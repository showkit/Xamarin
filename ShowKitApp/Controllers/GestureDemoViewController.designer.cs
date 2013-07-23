// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace ShowKitApp
{
	[Register ("GestureDemoViewController")]
	partial class GestureDemoViewController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel labelView { get; set; }

		[Action ("alertAction:")]
		partial void alertAction (MonoTouch.Foundation.NSObject sender);

		[Action ("blueAction:")]
		partial void blueAction (MonoTouch.Foundation.NSObject sender);

		[Action ("greenAction:")]
		partial void greenAction (MonoTouch.Foundation.NSObject sender);

		[Action ("hiddenAction:")]
		partial void hiddenAction (MonoTouch.Foundation.NSObject sender);

		[Action ("miscellaneousAction:")]
		partial void miscellaneousAction (MonoTouch.Foundation.NSObject sender);

		[Action ("redAction:")]
		partial void redAction (MonoTouch.Foundation.NSObject sender);

		[Action ("segmentedControlAction:")]
		partial void segmentedControlAction (MonoTouch.Foundation.NSObject sender);

		[Action ("sliderAction:")]
		partial void sliderAction (MonoTouch.Foundation.NSObject sender);

		[Action ("stepperAction:")]
		partial void stepperAction (MonoTouch.Foundation.NSObject sender);

		[Action ("switchAction:")]
		partial void switchAction (MonoTouch.Foundation.NSObject sender);

		[Action ("whiteAction:")]
		partial void whiteAction (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (labelView != null) {
				labelView.Dispose ();
				labelView = null;
			}
		}
	}
}
