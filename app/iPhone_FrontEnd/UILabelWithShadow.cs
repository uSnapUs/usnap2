using System;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Drawing;

namespace iPhone_FrontEnd
{
	public class UILabelWithShadow:UILabel
	{

		public override void Draw (System.Drawing.RectangleF rect)
		{
			var myShadowOffset = new SizeF(4,-4);
			var myColorValues = new []{0f,0f,0f,.8f};
			var myContext = UIGraphics.GetCurrentContext();
			myContext.SaveState();
			var myColorSpace = CGColorSpace.CreateDeviceRGB();
			var myColor = new CGColor(myColorSpace,myColorValues);
			myContext.SetShadowWithColor(myShadowOffset,5,myColor);
			base.Draw(rect);
			myColor.Dispose();
			myColorSpace.Dispose();
			myContext.Dispose();
		}


	}
}

