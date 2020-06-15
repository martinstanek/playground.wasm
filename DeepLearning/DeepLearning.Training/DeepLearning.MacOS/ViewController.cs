using System;
using AppKit;
using Foundation;

namespace DeepLearning.MacOS
{
    public partial class ViewController : NSViewController
    {
        public ViewController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            imgView.RegisterForDraggedTypes(new string[] { "*.jpg" });
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
            }
        }
    }
}
