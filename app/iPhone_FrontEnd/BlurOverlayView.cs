using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;
namespace iPhone_FrontEnd
{
	public class BlurOverlayView:UIView
	{
		float _radius;

		RectangleF _holeRect;

		PointF _circleCenter;

		public BlurOverlayView (RectangleF frame):base(frame)
		{
			_radius = 80f;
			this.UserInteractionEnabled = false;
			this.Opaque = false;
			var center = new PointF (frame.GetMidX (), frame.GetMidY ());
			_holeRect = new RectangleF(center.X-_radius,center.Y-_radius,_radius*2,_radius*2);
		}
		PointF CircleCentre{
			set{
				_circleCenter = value;
				_holeRect = new RectangleF(_circleCenter.X-_radius,_circleCenter.Y-_radius,_radius*2,_radius*2);
				SetNeedsDisplay();
			}
		}
		float Radius {
			set {
				_radius = value;
				var center = new PointF (_holeRect.GetMidX (), _holeRect.GetMidY ());
				_holeRect = new RectangleF(center.X-_radius,center.Y-_radius,_radius*2,_radius*2);
				SetNeedsDisplay();
			}
		}
		public override void DrawRect (RectangleF area, UIViewPrintFormatter formatter)
		{
			var context = UIGraphics.GetCurrentContext ();
			var locations = new []{0f,1f};
			var components = new []{1f,1f,1f,0f,1f,1f,1f,1f};
			var colourSpace = CGColorSpace.CreateDeviceRGB ();
			var gradient = new CGGradient (colourSpace, components, locations);
			var center = new PointF (_holeRect.GetMidX (), _holeRect.GetMidY ());
			context.DrawRadialGradient (gradient, center, _radius-25,center,_radius, CGGradientDrawingOptions.DrawsAfterEndLocation);
			colourSpace.Dispose ();
			gradient.Dispose ();
		}
	}
}

