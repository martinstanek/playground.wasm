// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace DeepLearning.MacOS
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSImageView imgView { get; set; }

		[Outlet]
		AppKit.NSTextField lblImage { get; set; }

		[Action ("imageChange:")]
		partial void imageChange (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (imgView != null) {
				imgView.Dispose ();
				imgView = null;
			}

			if (lblImage != null) {
				lblImage.Dispose ();
				lblImage = null;
			}
		}
	}
}
