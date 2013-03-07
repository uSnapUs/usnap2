using System;
using MonoTouch.UIKit;

namespace iPhone_FrontEnd
{
	public class CreateEventView:UIView
	{
		public CreateEventView ()
		{
			InitView();
			WireEvents();
		}

		UIImageView _backgroundFrame;

		UIView _landingView;

		UIImageView _logoImageView;

		UIButton _backButton;

		public EventHandler<EventArgs> BackButtonPressed;

		UIImageView _eventNameView;

		UITextField _eventNameField;

		UIImageView _locationView;

		UITextField _locationField;

		UIImageView _eventCodeView;

		UITextField _eventCodeField;

		UIButton _publicToggle;

		UIButton _createButton;

		void InitView ()
		{
			BecomeFirstResponder();
			_landingView = new UIView();
			_backgroundFrame = new UIImageView{
				Image = UIImage.FromFile(@"background.png"),
				ContentMode = UIViewContentMode.ScaleAspectFill,
			};
			_logoImageView = new UIImageView{
				Image = UIImage.FromFile(@"logo.png"),
				ContentMode = UIViewContentMode.ScaleAspectFit,
			};

			_backButton = new UIButton{};

			_backButton.SetBackgroundImage(UIImage.FromFile(@"Button_Back.png"),UIControlState.Normal);



			//event name field
			_eventNameView = new UIImageView{
				Image = UIImage.FromFile(@"Field_Background.png"),
				ContentMode=UIViewContentMode.Center,
				Hidden = false,
				UserInteractionEnabled=true
				
			};
			
			_eventNameField = new UITextField{
				Font = UIFont.FromName("ProximaNova-Bold",24),
				Placeholder = "Event Name",
				TextAlignment = UITextAlignment.Center,
				Enabled =true,
				EnablesReturnKeyAutomatically = true,
				ReturnKeyType = UIReturnKeyType.Next,
				AutocapitalizationType = UITextAutocapitalizationType.AllCharacters,
				
			};
			

			_eventNameField.ShouldReturn = delegate(UITextField textField) {
				return true;
			};
			_eventNameView.AddSubview(_eventNameField);

			//location field
			_locationView = new UIImageView{
				Image = UIImage.FromFile(@"Field_Background.png"),
				ContentMode=UIViewContentMode.Center,
				Hidden = false,
				UserInteractionEnabled=true
				
			};
			
			_locationField = new UITextField{
				Font = UIFont.FromName("ProximaNova-Bold",24),
				Placeholder = "Location",
				TextAlignment = UITextAlignment.Center,
				Enabled =true,
				EnablesReturnKeyAutomatically = true,
				ReturnKeyType = UIReturnKeyType.Next,
				AutocapitalizationType = UITextAutocapitalizationType.AllCharacters,
				
			};
			
			
			_locationField.ShouldReturn = delegate(UITextField textField) {
				return true;
			};
			_locationView.AddSubview(_locationField);


			//event code field
			_eventCodeView = new UIImageView{
				Image = UIImage.FromFile(@"Field_Background.png"),
				ContentMode=UIViewContentMode.Center,
				Hidden = false,
				UserInteractionEnabled=true
				
			};
			
			_eventCodeField = new UITextField{
				Font = UIFont.FromName("ProximaNova-Bold",24),
				Placeholder = "Event Code",
				TextAlignment = UITextAlignment.Center,
				Enabled =true,
				EnablesReturnKeyAutomatically = true,
				ReturnKeyType = UIReturnKeyType.Next,
				AutocapitalizationType = UITextAutocapitalizationType.AllCharacters,
				
			};
			
			
			_eventCodeField.ShouldReturn = delegate(UITextField textField) {
				return true;
			};
			_eventCodeView.AddSubview(_eventCodeField);

			_publicToggle = new UIButton{
				AdjustsImageWhenHighlighted = false
			};
			_publicToggle.SetBackgroundImage(UIImage.FromFile(@"button_toggle_public.png"),UIControlState.Normal);
			_publicToggle.SetBackgroundImage(UIImage.FromFile(@"button_toggle_private.png"),UIControlState.Selected);

			_createButton = new UIButton{
			};
			_createButton.SetBackgroundImage(UIImage.FromFile(@"Button_Create_Blue.png"),UIControlState.Normal);
			this._landingView.ClipsToBounds = true;
			this.ClipsToBounds = true;
			this.AutosizesSubviews = true;
			//_backgroundFrame.AutoresizingMask = UIViewAutoresizing.FlexibleHeight|UIViewAutoresizing.FlexibleWidth;




			this.AddSubview(_landingView);
			
			_landingView.AddSubview(_backgroundFrame);
			_landingView.AddSubview(_logoImageView);
			_landingView.AddSubview(_backButton);
			_landingView.AddSubview(_eventNameView);
			_landingView.AddSubview(_locationView);
			_landingView.AddSubview(_eventCodeView);
			_landingView.AddSubview(_publicToggle);
			_landingView.AddSubview(_createButton);

		}
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			//make the root view full size
			var mainBounds = this.Bounds;
			var height = this.Bounds.Size.Height;
			var width = this.Bounds.Size.Width;
			
			//	this.Frame = new System.Drawing.RectangleF(0,this.Bounds.Y-viewOffset,width,height);
		
			this._landingView.Frame = mainBounds;			
			this._backgroundFrame.Frame = this._landingView.Bounds;
			//landscape
			var horizontalMiddle = width/2;
			if (width > height) {
				_logoImageView.Frame = new System.Drawing.RectangleF((horizontalMiddle-(136/2))+2,10,136,61);
				
			} else {
				_logoImageView.Frame = new System.Drawing.RectangleF((horizontalMiddle-(271/2))+1,10,271,122);
				
			}

			_backButton.Frame = new System.Drawing.RectangleF(10,10,25,25);
			_eventNameView.Frame = new System.Drawing.RectangleF((horizontalMiddle-(234/2)),_logoImageView.Frame.Y+_logoImageView.Frame.Height+5,234,63);
			var fieldBounds = _eventNameView.Bounds;
			_eventNameField.Frame = new System.Drawing.RectangleF(0,fieldBounds.Y+15,fieldBounds.Width,fieldBounds.Height-20);

			_locationView.Frame = new System.Drawing.RectangleF((horizontalMiddle-(234/2)),_eventNameView.Frame.Y+_eventNameView.Frame.Height+3,234,63);
			fieldBounds = _locationView.Bounds;
			_locationField.Frame = new System.Drawing.RectangleF(0,fieldBounds.Y+15,fieldBounds.Width,fieldBounds.Height-20);

			_eventCodeView.Frame = new System.Drawing.RectangleF((horizontalMiddle-(234/2)),_locationView.Frame.Y+_locationView.Frame.Height+3,234,63);
			fieldBounds = _eventCodeView.Bounds;
			_eventCodeField.Frame = new System.Drawing.RectangleF(0,fieldBounds.Y+15,fieldBounds.Width,fieldBounds.Height-20);

			_publicToggle.Frame = new System.Drawing.RectangleF((horizontalMiddle-(234/2)),_eventCodeView.Frame.Y+_eventCodeView.Frame.Height+3,234,58);
			_createButton.Frame = new System.Drawing.RectangleF((horizontalMiddle-(234/2)),_publicToggle.Frame.Y+_publicToggle.Frame.Height+20,234,63);
		}

		void UnwireEvents ()
		{
			_backButton.TouchUpInside-=OnBackButtonPress;
			_publicToggle.TouchUpInside -= OnTogglePress;
		}
		void WireEvents ()
		{
			_backButton.TouchUpInside+=OnBackButtonPress;
			_publicToggle.TouchUpInside += OnTogglePress;
		}
		protected override void Dispose (bool disposing)
		{
			UnwireEvents();
			_backButton = null;
			_backgroundFrame = null;
			_landingView = null;
			_logoImageView = null;
			_eventCodeField = null;
			_eventCodeView = null;
			_eventNameField = null;
			_eventNameView = null;
			_locationField = null;
			_locationView = null;
			_publicToggle = null;
			_createButton = null;
			base.Dispose (disposing);
		}
		void OnBackButtonPress (object sender, EventArgs e)
		{
			_eventCodeField.ResignFirstResponder();
			_eventNameField.ResignFirstResponder();
			_locationField.ResignFirstResponder();
			if (BackButtonPressed != null) {
				BackButtonPressed.Invoke(this,e);
			}
		}

		void OnTogglePress (object sender, EventArgs e)
		{
			_publicToggle.Selected = !_publicToggle.Selected;
		}
	}
}

