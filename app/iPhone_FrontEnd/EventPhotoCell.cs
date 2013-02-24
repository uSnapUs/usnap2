using System;
using MonoTouch.UIKit;

namespace iPhone_FrontEnd
{
	public class EventPhotoCell:UITableViewCell
	{
		public EventPhotoCell (string reuseIdentifier):base(UITableViewCellStyle.Default,reuseIdentifier)
		{
			InitView();
		}

		UIImageView _photoView;

		UIImageView _frameView;

		void InitView ()
		{
			_frameView = new UIImageView(){
				Image = UIImage.FromFile(@"photo_frame.png")
			};
			_photoView = new UIImageView(){
				Image = UIImage.FromFile(@"placeholder.png")
			};
			_frameView.AddSubview(_photoView);
			this.AddSubview(_frameView);
		}
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			var width = this.Bounds.Width;
			var height = 370;
			var frame = this.Frame;
			this.Frame = new System.Drawing.RectangleF(frame.X,frame.Y,width,height);
			this._frameView.Frame = new System.Drawing.RectangleF((width/2)-(294/2),(height/2)-(352/2)+5,294,352);
			_photoView.Frame = new System.Drawing.RectangleF((294/2)-(275/2),10,275,299);
		}
	}
}

