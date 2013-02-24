using System;
using MonoTouch.UIKit;

namespace iPhone_FrontEnd
{
	public class CreateEventViewController:UIViewController
	{
		CreateEventView _createView;

		public CreateEventViewController ():base()
		{
			_createView = new CreateEventView();
			_createView.BackButtonPressed+=OnBackPress;
			View = _createView;
		}

		void OnBackPress (object sender, EventArgs e)
		{
			var landingView = new LandingPageViewController();
			landingView.ModalTransitionStyle = UIModalTransitionStyle.FlipHorizontal;
			PresentViewController(landingView,true,()=>{Dispose ();});
		}

		protected override void Dispose (bool disposing)
		{
			_createView.BackButtonPressed-=OnBackPress;
			_createView = null;
			base.Dispose (disposing);
		}
	}
}

