using System;
using MonoTouch.UIKit;

namespace iPhone_FrontEnd
{
	public class LandingPageView:UIView
	{
		UIImageView _backgroundFrame;
		UIImageView _logoImageView;

		UIImageView _taglineView;

		public LandingPageView ():base()
		{
			this.InitViews();

		}
		public override void LayoutSubviews ()
		{

			base.LayoutSubviews ();
			//make the root view full size
			this.Frame = this.Superview.Bounds;
			var height = this.Bounds.Height;
			var width = this.Bounds.Width;
			Console.WriteLine ("height:{0},width:{1}", height, width);
			this._backgroundFrame.Frame = this.Bounds;
			//landscape
			var horizontalMiddle = width/2;
			if (width > height) {

				_logoImageView.Frame = new System.Drawing.RectangleF((horizontalMiddle-(136/2))+2,20,136,61);
				_taglineView.Frame = new System.Drawing.RectangleF((horizontalMiddle-(236/2)),79,236,18);
			} else {
				
					_logoImageView.Frame = new System.Drawing.RectangleF((horizontalMiddle-(271/2))+1,81,271,122);
				_taglineView.Frame = new System.Drawing.RectangleF((horizontalMiddle-(236/2)),211,236,18);
			}
		}

		void InitViews ()
		{
			_backgroundFrame = new UIImageView{
				Image = UIImage.FromFile(@"background.png"),
				ContentMode = UIViewContentMode.ScaleAspectFill,
			};
			_logoImageView = new UIImageView{
				Image = UIImage.FromFile(@"logo.png"),
				ContentMode = UIViewContentMode.ScaleAspectFit,
			};
			_taglineView = new UIImageView{
				Image = UIImage.FromFile(@"tagline.png"),
				ContentMode = UIViewContentMode.Center
			};
			this.AutosizesSubviews = true;
			this.AutoresizingMask = UIViewAutoresizing.FlexibleWidth|UIViewAutoresizing.FlexibleHeight;
			_backgroundFrame.AutoresizingMask = UIViewAutoresizing.FlexibleHeight|UIViewAutoresizing.FlexibleWidth;
			this.AddSubview(_backgroundFrame);
			this.AddSubview(_logoImageView);
			this.AddSubview(_taglineView);
		}

	}
}

