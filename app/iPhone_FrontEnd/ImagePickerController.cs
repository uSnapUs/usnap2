using System;
using MonoTouch.UIKit;
using MonoTouch.CoreFoundation;
using GPUImage;
using MonoTouch.AVFoundation;
using MonoTouch.Foundation;

namespace iPhone_FrontEnd
{
	public class ImagePickerController:UIViewController
	{
		ImagePickerView _imagePickerView;
		bool _hasBlur;

		bool _isStatic;

		GPUImageStillCamera _stillCamera;
		GPUImageCropFilter _cropFilter;
		GPUImageFilter _filter;
		GPUImageGaussianSelectiveBlurFilter _blurFilter;
		GPUImagePicture _staticPicture;
		UIImageOrientation _staticPictureOriginalOrientation;

		public ImagePickerController ():base()
		{
			_imagePickerView =new ImagePickerView();
			_imagePickerView.LibraryButtonPressed += SwitchToLibrary;
			this.View = _imagePickerView;
			ViewDidLoad ();
		}
		protected override void Dispose (bool disposing)
		{
			_filter = null;
			_staticPicture = null;
			_stillCamera = null;

			_imagePickerView.LibraryButtonPressed -= SwitchToLibrary;
			_imagePickerView = null;

			base.Dispose (disposing);

		}
		public override void ViewDidLoad ()
		{
			WantsFullScreenLayout = true;
			_staticPictureOriginalOrientation = UIImageOrientation.Up;
			SetFilter (4);
			SetupCamera();
		}
		public void OnPinch(UIPinchGestureRecognizer pinchGesture){
			if (_hasBlur) {
				var midpoint = pinchGesture.LocationInView (pinchGesture.View);
				//GPUImageGaussianSelectiveBlurFilter gpu = (GPUImageGaussianSelectiveBlurFilter*)_blurFilter;
				/*
				if (pinchGesture.State == UIGestureRecognizerState.Began) {
				this.ShowBlurOverlay=true;
				//[gpu setBlurSize:0.0f];
				if (_isStatic) {
					_staticPicture.ProcessImage;
				}

			}
			
			if ([sender state] == UIGestureRecognizerStateBegan || [sender state] == UIGestureRecognizerStateChanged) {
				[gpu setBlurSize:0.0f];
				[gpu setExcludeCirclePoint:CGPointMake(midpoint.x/320.0f, midpoint.y/320.0f)];
				self.blurOverlayView.circleCenter = CGPointMake(midpoint.x, midpoint.y);
				CGFloat radius = MAX(MIN(sender.scale*[gpu excludeCircleRadius], 0.6f), 0.15f);
				self.blurOverlayView.radius = radius*320.f;
				[gpu setExcludeCircleRadius:radius];
				sender.scale = 1.0f;
			}
			
			if ([sender state] == UIGestureRecognizerStateEnded) {
				[gpu setBlurSize:kStaticBlurSize];
				[self showBlurOverlay:NO];
				if (isStatic) {
					[staticPicture processImage];
				}
			}
			*/
			} else {

			}

		}
		void RunOnMainQueueWithoutDeadlocking(NSAction action){
			if (NSThread.IsMain) {
				action.Invoke ();
			}else {
				DispatchQueue.MainQueue.DispatchSync(action);
			}
		}

		void PrepareFilter ()
		{
			Console.WriteLine ("Preparing filter");
			if (!UIImagePickerController.IsSourceTypeAvailable (UIImagePickerControllerSourceType.Camera)) {
				_isStatic = true;
			}
			if (!_isStatic) {
				PrepareLiveFilter();
		} else {
				PrepareStaticFilter();
		}

		}

		void PrepareLiveFilter ()
		{
			_stillCamera.AddTarget(_cropFilter);
			_cropFilter.AddTarget(_filter);
			if (_hasBlur) {
				_filter.AddTarget(_blurFilter);
				_blurFilter.AddTarget(_imagePickerView.ImageView);
			} else {
				_filter.AddTarget (this._imagePickerView.ImageView);
			}
		}

		void PrepareStaticFilter ()
		{
			if (_staticPicture == null) {
				NSTimer.CreateScheduledTimer (0.5,()=>{SwitchToLibrary(this,null);});
				return;
			}

			_staticPicture.AddTarget(_filter);
			if(_hasBlur){
				_filter.AddTarget(_blurFilter);
				_blurFilter.AddTarget(_imagePickerView.ImageView);
			}
			else{
					_filter.AddTarget(_imagePickerView.ImageView);
			}
			var imageViewRotationMode = GPUImageRotationMode.kGPUImageNoRotation;
			switch (_staticPictureOriginalOrientation) {
			case UIImageOrientation.Left:
				imageViewRotationMode = GPUImageRotationMode.kGPUImageRotateLeft;
				break;
			case UIImageOrientation.Right:
				imageViewRotationMode = GPUImageRotationMode.kGPUImageRotateRight;
				break;
			case UIImageOrientation.Down:
				imageViewRotationMode = GPUImageRotationMode.kGPUImageRotate180;
				break;
			default:
				imageViewRotationMode = GPUImageRotationMode.kGPUImageNoRotation;
				break;

			}
			_imagePickerView.ImageView.SetInputRotationAtIndex (imageViewRotationMode, 0);
			_staticPicture.ProcessImage ();
		}

		void SetupCamera ()
		{
			if (UIImagePickerController.IsSourceTypeAvailable (UIImagePickerControllerSourceType.Camera)) {
				DispatchQueue.MainQueue.DispatchAsync (() => {
					_stillCamera = new GPUImageStillCamera (AVCaptureSession.PresetPhoto, AVCaptureDevicePosition.Back);
					_stillCamera.OutputImageOrientation = UIInterfaceOrientation.Portrait;	
					RunOnMainQueueWithoutDeadlocking (() => {
						_stillCamera.StartCameraCapture ();
						if (_stillCamera.InputCamera.TorchAvailable) {
							_imagePickerView.FlashEnabled = true;
						} else {
							_imagePickerView.FlashEnabled = false;
						}
						PrepareFilter ();
					});
				});
			} else {
				RunOnMainQueueWithoutDeadlocking(()=>{PrepareFilter();});
			}
		}
		void SetFilter(int index)
		{
			switch (index) {
			case 1:
				var contrastFilter = new GPUImageContrastFilter ();
				contrastFilter.Contrast=1.75f;
				_filter = contrastFilter;
				break;
			case 2: 
				_filter = new GPUImageToneCurveFilter ("crossprocess");    
				break;
			case 3: 
				_filter = new GPUImageToneCurveFilter (@"02");
				break;
			case 4: 
				_filter = new GPUGrayscaleContrastFilter ();
				break;
			case 5: 
				_filter = new GPUImageToneCurveFilter (@"17");
				break;
			case 6: 
				_filter = new GPUImageToneCurveFilter (@"aqua");
				break;
			case 7: 
				_filter = new GPUImageToneCurveFilter (@"yellow-red");
				break;
			case 8: 
				_filter = new GPUImageToneCurveFilter (@"06");
				break;
			case 9: 
				_filter = new GPUImageToneCurveFilter (@"purple-green");
				break;
			default:
				_filter = new GPUImageFilter ();
				break;
			}
			
			
		}

		void RemoveAllTargets ()
		{
			_stillCamera.RemoveAllTargets ();
			_staticPicture.RemoveAllTargets ();
			_cropFilter.RemoveAllTargets ();

			
			//regular filter
			_filter.RemoveAllTargets ();

			
			//blur
			_blurFilter.RemoveAllTargets ();


		}

		void SwitchToLibrary (object sender, EventArgs e)
		{
			if (!_isStatic) {
				_stillCamera.StopCameraCapture();
				this.RemoveAllTargets();
			}
			var imagePickerController = new UIImagePickerController ();
			imagePickerController.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			imagePickerController.AllowsEditing = true;
			PresentViewController (imagePickerController, true, null);
		}
	}
}

