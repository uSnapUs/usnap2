// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace iPhone_FrontEnd
{
	[Register ("LandingPageViewController")]
	partial class LandingPageViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView Logo { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView LandingTagline { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton FindButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton CreateButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton MyEventsButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton InfoButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView LandingBackground { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView LandingPageView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton BackButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView FindTagline { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton JoinButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField EventIdField { get; set; }

		[Action ("FindPressed:")]
		partial void FindPressed (MonoTouch.Foundation.NSObject sender);

		[Action ("BackPressed:")]
		partial void BackPressed (MonoTouch.Foundation.NSObject sender);

		[Action ("OnJoinEditBegin:")]
		partial void OnJoinEditBegin (MonoTouch.Foundation.NSObject sender);

		[Action ("OnJoinExit:")]
		partial void OnJoinExit (MonoTouch.Foundation.NSObject sender);

		[Action ("FindNearbyPressed:")]
		partial void FindNearbyPressed (MonoTouch.Foundation.NSObject sender);

		[Action ("CreatePressed:")]
		partial void CreatePressed (MonoTouch.Foundation.NSObject sender);

		[Action ("MyEventsPressed:")]
		partial void MyEventsPressed (MonoTouch.Foundation.NSObject sender);

		[Action ("InfoPressed:")]
		partial void InfoPressed (MonoTouch.Foundation.NSObject sender);

		[Action ("JoinPressed:")]
		partial void JoinPressed (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (Logo != null) {
				Logo.Dispose ();
				Logo = null;
			}

			if (LandingTagline != null) {
				LandingTagline.Dispose ();
				LandingTagline = null;
			}

			if (FindButton != null) {
				FindButton.Dispose ();
				FindButton = null;
			}

			if (CreateButton != null) {
				CreateButton.Dispose ();
				CreateButton = null;
			}

			if (MyEventsButton != null) {
				MyEventsButton.Dispose ();
				MyEventsButton = null;
			}

			if (InfoButton != null) {
				InfoButton.Dispose ();
				InfoButton = null;
			}

			if (LandingBackground != null) {
				LandingBackground.Dispose ();
				LandingBackground = null;
			}

			if (LandingPageView != null) {
				LandingPageView.Dispose ();
				LandingPageView = null;
			}

			if (BackButton != null) {
				BackButton.Dispose ();
				BackButton = null;
			}

			if (FindTagline != null) {
				FindTagline.Dispose ();
				FindTagline = null;
			}

			if (JoinButton != null) {
				JoinButton.Dispose ();
				JoinButton = null;
			}

			if (EventIdField != null) {
				EventIdField.Dispose ();
				EventIdField = null;
			}
		}
	}
}
