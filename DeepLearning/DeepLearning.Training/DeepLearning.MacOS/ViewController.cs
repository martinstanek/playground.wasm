using System;
using System.IO;
using AppKit;
using DeepLearning.Server.Client;
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
            ((ImageView) imgView).DraggingDidEnd += OnDraggingEnd;
        }

        private async void OnDraggingEnd(object sender, string path)
        {
            var classifierApi = new ClassifierApi("http://localhost:5000");

            using var fileStream = new FileStream(path, FileMode.Open);

            var whatIsOnImage = await classifierApi.ClasisifyAsync(fileStream, Path.GetFileName(path));

            lblImage.StringValue = "The image is: " + whatIsOnImage;
        }

        public override NSObject RepresentedObject => base.RepresentedObject;
    }
}
