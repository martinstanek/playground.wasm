using System;
using AppKit;
using Foundation;
// using DeepLearning.Predication;

namespace DeepLearning.MacOS
{
    public partial class ViewController : NSViewController
    {
        public ViewController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            imgView.RegisterForDraggedTypes(new string[] { "*.jpg" });
            ((ImageView) imgView).DraggingDidEnd += OnDraggingEnd;
        }

        private void OnDraggingEnd(object sender, string path)
        {
            // var predication = new Predicator();

            lblImage.StringValue = path;
        }

        public override NSObject RepresentedObject => base.RepresentedObject;
    }
}
