using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace iPhone_FrontEnd
{
	public class EventDashboardView:UIView
	{
		UIView _topBarView;
		UIImageView _logoImageView;
		UITableView _stream;
		UIButton _backButton;

		UIButton _cameraButton;

		UILabel _tableTitleLable;

		public EventHandler<EventArgs> BackButtonPressed;
		public EventHandler<EventArgs> CameraButtonPressed;

		public EventDashboardView ()
		{
			InitViews();
			WireEvents();
		}
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews();
			var height = this.Bounds.Height;
			var width = this.Bounds.Width;
			_topBarView.Frame = new System.Drawing.RectangleF(0,0,width,51);
			_logoImageView.Frame = new System.Drawing.RectangleF((width/2)-(94/2),3,94,44);
			_backButton.Frame = new System.Drawing.RectangleF(15,15,25,22);
			_cameraButton.Frame = new System.Drawing.RectangleF(_topBarView.Frame.Width-29-15,15,29,22);
			_stream.Frame=new System.Drawing.RectangleF(0,51,width,height-51);
			var tableHeaderViewFrame = _stream.TableHeaderView.Frame;
			tableHeaderViewFrame.Height= 90;
			_tableHeaderView.Frame = tableHeaderViewFrame;
			_stream.TableHeaderView = _tableHeaderView;
			_tableTitleLable.Frame = new System.Drawing.RectangleF(0,27,width,35);
		}

		UIView _tableHeaderView;
		
		void InitViews ()
		{
			this.ClipsToBounds = true;
			this.AutosizesSubviews = true;

			this.BackgroundColor = UIColor.White;
			_tableHeaderView = new UIView(new System.Drawing.RectangleF(0,0,320,90)) {BackgroundColor=UIColor.Black,ClearsContextBeforeDrawing=true,Opaque=true};
			_stream = new UITableView(){
				TableHeaderView = _tableHeaderView,
				BackgroundColor = UIColor.FromRGB(239,237,236),
				SeparatorColor = UIColor.Clear,
				RowHeight=370
			};
			//_stream.AddSubview(_tableHeaderView);
			_topBarView = new UIView{
				BackgroundColor = UIColor.FromRGB(17,186,188)
				
			};
			_logoImageView = new UIImageView{
				Image = UIImage.FromFile(@"logo_small.png")
			};
			_backButton = new UIButton();
			_cameraButton = new UIButton();
			_backButton.SetBackgroundImage(UIImage.FromFile(@"Button_Topbar_Back.png"),UIControlState.Normal);
			_cameraButton.SetBackgroundImage(UIImage.FromFile(@"Button_Topbar_Camera.png"),UIControlState.Normal);
			_topBarView.Layer.ShadowOffset = new System.Drawing.SizeF(0, 2);
			_topBarView.Layer.ShadowRadius = 5;
			_topBarView.Layer.ShadowOpacity = 0.5f;
			//_topBarView.Layer.CornerRadius = 5;
			_tableTitleLable = new UILabel{
				Text = "Jeremy's 21st",
				TextColor = UIColor.White,
				ShadowOffset = new SizeF(0,2),
				ShadowColor = UIColor.Black,
				BackgroundColor = UIColor.Clear,
				TextAlignment = UITextAlignment.Center,
				Opaque = false,
				Font = UIFont.FromName("Gelato Script",30)

			};

			_tableHeaderView.AddSubview(_tableTitleLable);
			this.AddSubview(_stream);
			this.AddSubview(_topBarView);

			_topBarView.AddSubview(_logoImageView);
			_topBarView.AddSubview(_backButton);
			_topBarView.AddSubview(_cameraButton);
		}
		
		void OnBackButtonPress (object sender, EventArgs e)
		{
			if (this.BackButtonPressed!=null) {
				this.BackButtonPressed.Invoke(this,e);
			}
		}

		void OnCameraButonPress (object sender, EventArgs e)
		{
			if (this.CameraButtonPressed!=null) {
				this.CameraButtonPressed.Invoke(this,e);
			}
		}

		protected override void Dispose (bool disposing)
		{
			this.UnwireEvents();
			_topBarView = null;
			_logoImageView = null;
			_backButton = null;			
			
		}

		public UITableView TableView {
			get {
				return _stream;
			}
		}
		
		void WireEvents ()
		{
			
			_backButton.TouchUpInside+=OnBackButtonPress;
			_cameraButton.TouchUpInside+=OnCameraButonPress;
		}
		
		void UnwireEvents ()
		{
			
			_backButton.TouchUpInside-=OnBackButtonPress;
			_cameraButton.TouchUpInside+=OnCameraButonPress;
		}
	}
}

