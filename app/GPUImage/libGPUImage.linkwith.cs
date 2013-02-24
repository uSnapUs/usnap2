using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libGPUImage.a", LinkTarget.ArmV7, ForceLoad = true,Frameworks="CoreVideo CoreMedia OpenGLES QuartzCore AVFoundation UIKit Foundation")]