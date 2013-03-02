using System;
using MonoTouch.UIKit;
using GPUImage;
using MonoTouch.CoreFoundation;
using MonoTouch.AVFoundation;
using MonoTouch.Foundation;

namespace iPhone_FrontEnd
{
	public class UIPinchEventArgs:EventArgs{
		UIPinchGestureRecognizer _gesture;

		public UIPinchEventArgs(UIPinchGestureRecognizer gesture){
			this._gesture = gesture;
		}
		public UIPinchGestureRecognizer gesture
		{
			get{
				return _gesture;
			}
		}
	}
	public class UITapEventArgs:EventArgs{
		UITapGestureRecognizer _gesture;

		public UITapEventArgs(UITapGestureRecognizer gesture){
			this._gesture = gesture;
		}
		public UITapGestureRecognizer gesture
		{
			get{
				return _gesture;
			}
		}
	}
	public class ImagePickerView:UIView
	{
		GPUImageView _imageView;

		UIView _photoBar;

		UIView _topBar;

		UIButton _shutterButton;

		UIButton _filterButton;

		UIButton _libraryButton;

		UIButton _flashButton;

		UIButton _flipButton;

		UIButton _blurButton;

		UIButton _closeButton;

		UIButton _retakeButton;

		UIScrollView _filterScrollView;

		UIPinchGestureRecognizer _pinchGestureRecogniser;

		UITapGestureRecognizer _tapGestureRecogniser;

		UIImageView _focusView;

		UIImageView _filterScrollBackgroundView;

		BlurOverlayView _blurOverlayView;

		public EventHandler<UIPinchEventArgs> ImagePinched;
		public EventHandler<UITapEventArgs> ImageTapped;
		public EventHandler<EventArgs> LibraryButtonPressed;
		public EventHandler<EventArgs> FlipButtonPressed;
		public EventHandler<EventArgs> ShutterButtonPressed;
		public EventHandler<EventArgs> CloseButtonPressed;		
		public EventHandler<EventArgs> RetakeButtonPressed;
		public EventHandler<EventArgs> FilterButtonPressed;

		public ImagePickerView ():base()
		{
			InitView();
			SetupEvents ();
		}


		void InitView ()
		{
			BackgroundColor = UIColor.Gray;
			this.BackgroundColor = UIColor.FromPatternImage (UIImage.FromFile ("micro_carbon.png"));
			_photoBar = new UIView ();
			_photoBar.BackgroundColor = UIColor.FromPatternImage (UIImage.FromFile ("photo_bar.png"));
			_imageView = new GPUImageView();	
			_topBar = new UIView ();
			_topBar.BackgroundColor = UIColor.FromPatternImage (UIImage.FromFile ("photo_bar.png"));
			SetupPhotoBar ();
			SetupTopBar ();
			_focusView = new UIImageView (UIImage.FromFile ("focus-crosshair.png"));
			_focusView.Alpha = 0;
			_blurOverlayView = new BlurOverlayView (new System.Drawing.RectangleF(
				0, 0, _imageView.Frame.Width, _imageView.Frame.Height));
			_imageView.AddSubview (_blurOverlayView);



			_filterScrollView = new UIScrollView ();
			_filterScrollView.ContentInset = new UIEdgeInsets{Bottom = 0,Top = 0, Left = 0, Right = 0};
			_filterScrollView.ScrollIndicatorInsets = new UIEdgeInsets{Bottom = 0,Top = 0, Left = 0, Right = 0};
			_filterScrollView.ScrollEnabled = true;
			_filterScrollView.BouncesZoom = true;
			_filterScrollView.DelaysContentTouches = true;
			_filterScrollView.CanCancelContentTouches = true;
			_filterScrollView.UserInteractionEnabled = true;
			_filterScrollView.ClipsToBounds = true;
			_filterScrollView.AutosizesSubviews = true;
			_filterScrollView.ClearsContextBeforeDrawing = true;


			_filterScrollBackgroundView = new UIImageView (UIImage.FromFile ("dock_bg.png"));
			_filterScrollBackgroundView.Opaque = true;
			_filterScrollBackgroundView.ClearsContextBeforeDrawing = true;
			_filterScrollBackgroundView.AutosizesSubviews = true;


			this.AddSubview (_filterScrollBackgroundView);
			this.AddSubview (_filterScrollView);

			this.AddSubview (_photoBar);
			this.AddSubview(_imageView);
			this.AddSubview (_focusView);
			this.AddSubview (_topBar);

			_pinchGestureRecogniser = new UIPinchGestureRecognizer ((gesture)=>OnPinch(gesture));
			_tapGestureRecogniser = new UITapGestureRecognizer((gesture)=>OnTap(gesture));
			_imageView.AddGestureRecognizer (_tapGestureRecogniser);

		
		}

		void SetupEvents ()
		{
			_libraryButton.TouchUpInside += OnLibraryPress;
			_flipButton.TouchUpInside+=OnFlipPress;
			_shutterButton.TouchUpInside += OnShutterPress;
			_closeButton.TouchUpInside += OnClosePress;
			_retakeButton.TouchUpInside += OnRetakePress;
			_filterButton.TouchUpInside += OnFilterButtonPress;
		}

		void RemoveEvents ()
		{
			_libraryButton.TouchUpInside -= OnLibraryPress;
			_flipButton.TouchUpInside -= OnFlipPress;
			_shutterButton.TouchUpInside -= OnShutterPress;
			_closeButton.TouchUpInside -= OnClosePress;
			_retakeButton.TouchUpInside -= OnRetakePress;
			_filterButton.TouchUpInside -= OnFilterButtonPress;
		}

		protected override void Dispose (bool disposing)
		{

			RemoveEvents ();
			_pinchGestureRecogniser.Dispose ();
			_tapGestureRecogniser.Dispose ();
			_pinchGestureRecogniser = null;
			_tapGestureRecogniser = null;
			_topBar = null;
			_imageView = null;
			_libraryButton = null;
			_blurButton = null;
			_blurOverlayView = null;
			_closeButton = null;
			_filterButton = null;
			_flashButton = null;
			_flipButton = null;
			_focusView = null;
			_photoBar = null;
			_shutterButton = null;
			_topBar = null;
			base.Dispose (disposing);
		}


		void SetupTopBar ()
		{
			_flashButton = new UIButton ();
			_flashButton.SetImage (UIImage.FromFile ("flash-off.png"), UIControlState.Normal);
			_flashButton.SetImage (UIImage.FromFile ("flash.png"), UIControlState.Selected);
			_topBar.AddSubview (_flashButton);
			_flipButton = new UIButton ();
			_flipButton.SetImage (UIImage.FromFile ("front-camera.png"), UIControlState.Normal);

			_topBar.AddSubview (_flipButton);
			_blurButton = new UIButton ();
			_blurButton.SetImage (UIImage.FromFile ("blur.png"), UIControlState.Normal);
			_blurButton.SetImage (UIImage.FromFile ("blur-on.png"), UIControlState.Selected);
			_blurButton.Selected = false;
			_topBar.AddSubview (_blurButton);
			_closeButton = new UIButton ();
			_closeButton.SetImage (UIImage.FromFile ("close.png"), UIControlState.Normal);
			_topBar.AddSubview (_closeButton);
		}

		void SetupPhotoBar ()
		{
			_shutterButton = new UIButton();
			_shutterButton.SetImage (UIImage.FromFile ("camera-icon.png"), UIControlState.Normal);
			_shutterButton.SetBackgroundImage (UIImage.FromFile ("camera-button.png"), UIControlState.Normal);
			_filterButton = new UIButton ();
			_filterButton.SetImage (UIImage.FromFile ("filter-open.png"), UIControlState.Normal);
			_filterButton.SetImage (UIImage.FromFile ("filter-closed.png"), UIControlState.Selected);
			_filterButton.Selected = false;
			_libraryButton = new UIButton ();
			_libraryButton.SetImage (UIImage.FromFile ("library.png"), UIControlState.Normal);
			_retakeButton = new UIButton ();
			_retakeButton.SetTitle ("Retake", UIControlState.Normal);
			_retakeButton.Hidden = true;
			_retakeButton.SetBackgroundImage (UIImage.FromFile("camera-button.png"),UIControlState.Normal);
			_photoBar.AddSubview (_shutterButton);
			_photoBar.AddSubview (_filterButton);
			_photoBar.AddSubview (_libraryButton);
			_photoBar.AddSubview (_retakeButton);
		}


		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			var width = this.Bounds.Width;
			var height = this.Bounds.Height;
			_topBar.Frame = new System.Drawing.RectangleF (0, 0, width, 44);
			_flashButton.Frame = new System.Drawing.RectangleF (57, 0, 44, 44);
			_flipButton.Frame = new System.Drawing.RectangleF (136, 3, 50, 41);
			_blurButton.Frame = new System.Drawing.RectangleF (213, 0, 44, 44);
			_closeButton.Frame = new System.Drawing.RectangleF (277, 3, 40, 37);
			_imageView.Frame = new System.Drawing.RectangleF(0,78,320,320);
			_photoBar.Frame = new System.Drawing.RectangleF (0,height-44, width, 44);
			_shutterButton.Frame = new System.Drawing.RectangleF (115, 3, 90, 37);
			_libraryButton.Frame = new System.Drawing.RectangleF (-8, 3, 65, 37);
			_filterButton.Frame = new System.Drawing.RectangleF (263, 3, 65, 37);
			_retakeButton.Frame = new System.Drawing.RectangleF (11, 7, 71, 29);
			_filterScrollView.Frame = new System.Drawing.RectangleF (0, 437, 320, 75);
			_filterScrollBackgroundView.Frame = new System.Drawing.RectangleF (-12, 435, 344, 75);

		}

		public UIButton ShutterButton {
			get{
				return _shutterButton;
			}
		}

		public UIButton FlipButton {
			get{
				return _flipButton;
			}
		}

		public UIButton FlashButton {
			get{
				return _flashButton;
			}
		}

		public UIButton LibraryButton {
			get {
				return _libraryButton;
			}
		}
		
		public UIButton RetakeButton {
			get {
				return _retakeButton;
			}
		}
		
		public UIButton FiltersButton{
			get {
				return _filterButton;
			}
		}
		public UIScrollView FilterScrollView {
			get {
				return _filterScrollView;
			}
		}
		public UIImageView FilterScrollBackgroundView{
			get{
				return _filterScrollBackgroundView;
			}
		}
		public UIImageView FocusView{
			get{
				return _focusView;
			}
		}
		public GPUImageView ImageView {
			get {
				return _imageView;
			}
		}

	
		void OnLibraryPress (object sender, EventArgs e)
		{
			if (LibraryButtonPressed != null) {
				LibraryButtonPressed.Invoke(this,e);
			}
		}

		void OnFlipPress (object sender, EventArgs e)
		{
			if (FlipButtonPressed !=null) {
				FlipButtonPressed.Invoke(this,e);
			}
		}

		void OnShutterPress (object sender, EventArgs e)
		{
			if (ShutterButtonPressed != null) {
				ShutterButtonPressed.Invoke(this,e);
			}
		}

		void OnClosePress (object sender, EventArgs e)
		{
			if (CloseButtonPressed != null) {
				CloseButtonPressed.Invoke(this,e);
			}
		}

		void OnRetakePress (object sender, EventArgs e)
		{
			if (RetakeButtonPressed != null) {
				RetakeButtonPressed.Invoke(this,e);
			}
		}

		void OnFilterButtonPress (object sender, EventArgs e)
		{
			if (FilterButtonPressed != null) {
				FilterButtonPressed.Invoke(this,e);
			}
		}

		void OnPinch (UIPinchGestureRecognizer gesture)
		{
			if (ImagePinched != null) {
				ImagePinched.Invoke(this,new UIPinchEventArgs(gesture));
			}
			
		}
		
		void OnTap (UITapGestureRecognizer gesture)
		{
			if (ImageTapped != null) {
				ImageTapped.Invoke (this, new UITapEventArgs (gesture));
			}
		}
	}
}

