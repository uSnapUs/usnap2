// This file has been autogenerated from parsing an Objective-C header file added in Xcode.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace iPhone_FrontEnd
{
	public partial class ImediateSegue : UIStoryboardSegue
	{
		public ImediateSegue (IntPtr handle) : base (handle)
		{
		}
		public override void Perform ()
		{
			this.SourceViewController.PresentViewController(DestinationViewController,false,()=>{});
		}
	}
}