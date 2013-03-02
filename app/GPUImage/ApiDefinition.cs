using System;
using System.Drawing;

using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.AVFoundation;

namespace GPUImage
{
	[BaseType (typeof(UIView))]
	interface GPUImageView:GPUImageInput {
		[Export ("initWithFrame:")]
		IntPtr Constructor(RectangleF frame);
		
		[Export ("autoresizingMask")]
		UIViewAutoresizing AutoResizingMask { get; set;}
		
		[Export ("addSubView:")]
		void AddSubView(UIView view);

	

	}
	
	[BaseType (typeof(NSObject))]
	[Model]
	interface AVCaptureVideoDataOutputSampleBufferDelegate{
		
	}
	
	[BaseType (typeof(NSObject))]
	[Model]
	interface AVCaptureAudioDataOutputSampleBufferDelegate{
		
	}
	
	[BaseType (typeof(GPUImageOutput))]
	interface GPUImageVideoCamera : AVCaptureVideoDataOutputSampleBufferDelegate, AVCaptureAudioDataOutputSampleBufferDelegate{
		[Export("inputCamera")]
		AVCaptureDevice InputCamera{ get; }
		[Export("rotateCamera")]
		void RotateCamera();
		[Export("cameraPosition")]
		AVCaptureDevicePosition CameraPosition();
	}   
	
	[BaseType (typeof(GPUImageVideoCamera))]
	interface GPUImageStillCamera:GPUImageVideoCamera{

		[Export ("initWithSessionPreset:cameraPosition:")]
		IntPtr Constructor (NSString sessionPreset,AVCaptureDevicePosition cameraPosition);

		[Export ("outputImageOrientation")]
		UIInterfaceOrientation OutputImageOrientation { get; set;}
		
		[Export ("startCameraCapture")]
		void StartCameraCapture(); 
		[Export("stopCameraCapture")]
		void StopCameraCapture();
	}
	
	[BaseType (typeof(GPUImageSobelEdgeDetectionFilter))]
	interface GPUImageSketchFilter{ 
		[Export ("prepareForImageCapture")]
		void PrepareForImageCapture();
	}
	
	[BaseType (typeof(GPUImageTwoPassFilter))]
	interface GPUImageSobelEdgeDetectionFilter {
		[Export ("texelWidth")]
		float TexelWidth{ get; set;}
		
		[Export ("texelHeight")]
		float TexelHeight{ get; set;}
	}
	[BaseType (typeof(GPUImageFilter))]
	interface GPUGrayscaleContrastFilter {
	}
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageContrastFilter {
		[Export("contrast")]
		float Contrast{ get; set; }
	}
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageTwoPassFilter {
	}
	[BaseType(typeof(GPUImageFilter))]
	interface GPUImageCropFilter{
		[Export ("initWithCropRegion:")]
		IntPtr Constructor (RectangleF cropRegion);
	}
	[BaseType(typeof(GPUImageFilter))]
	interface GPUImageToneCurveFilter{
		[Export ("initWithACV:")]
		IntPtr Constructor (string curveFilename);
	}
	[BaseType (typeof(GPUImageOutput))]
	interface GPUImageFilter : GPUImageInput{

	}
	
	[BaseType (typeof(NSObject))]
	[Model]
	interface  GPUImageInput{
		[Export("setInputRotation:atIndex:")]
		void SetInputRotationAtIndex(GPUImageRotationMode newInputRotation,int textureIndex);
	}
	
	[BaseType (typeof(NSObject))]
	interface  GPUImageOutput{
		[Export ("addTarget:")]
		void AddTarget(NSObject newTarget);
		[Export("removeAllTargets")]
		void RemoveAllTargets();
		[Export("imageFromCurrentlyProcessedOutput")]
		UIImage ImageFromCurrentlyProcessedOutput();
		[Export("imageFromCurrentlyProcessedOutputWithOrientation:")]
		UIImage ImageFromCurrentlyProcessedOutputWithOrientation(UIImageOrientation imageOrientation);
	}
	[BaseType(typeof(GPUImageFilterGroup))]
	interface GPUImageGaussianSelectiveBlurFilter{
	}
	[BaseType(typeof(GPUImageOutput))]
	interface GPUImageFilterGroup:GPUImageInput,GPUImageTextureDelegate{
	}
	[BaseType(typeof(NSObject))]
	[Model]
	interface GPUImageTextureDelegate{
	}
	[BaseType(typeof(GPUImageOutput))]
	interface GPUImagePicture{
		[Export("processImage")]
		void ProcessImage();
		[Export("initWithImage:smoothlyScaleOutput:")]
		IntPtr Constructor(UIImage newImageSource,bool smoothlyScaleOutput);
	}

}

