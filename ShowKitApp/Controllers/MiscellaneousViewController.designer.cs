// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace ShowKitApp
{
	[Register ("MiscellaneousViewController")]
	partial class MiscellaneousViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton menuButton { get; set; }

		[Action ("actionSheetAction:")]
		partial void actionSheetAction (MonoTouch.Foundation.NSObject sender);

		[Action ("menuAction:")]
		partial void menuAction (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (menuButton != null) {
				menuButton.Dispose ();
				menuButton = null;
			}
		}
	}
}
