using System;
using AppKit;
using Foundation;

namespace DeepLearning.MacOS
{
	public partial class ImageView : NSImageView
	{
		public ImageView (IntPtr handle) : base (handle) { }

        public override NSDragOperation DraggingEntered(NSDraggingInfo sender)
        {
            return NSDragOperation.Copy;
        }

        public override bool PerformDragOperation(NSDraggingInfo sender)
        {
            var result = base.PerformDragOperation(sender);

            if (result)
            {
                var fileNamesXml = sender.DraggingPasteboard.GetStringForType("NSFilenamesPboardType");

                if (fileNamesXml != null)
                {
                    var propertyData = NSData.FromString(fileNamesXml);
                    var format = NSPropertyListFormat.Xml;
                    var ser = (NSArray) NSPropertyListSerialization.PropertyListWithData(propertyData, ref format, out var error);

                    if (error == null && ser != null && ser.Count > 0)
                    {
                        var path = NSString.FromHandle(ser.ValueAt(0));

                        DraggingDidEnd?.Invoke(this, path);
                    }
                }
            }

            return result;
        }

        public EventHandler<string> DraggingDidEnd;
    }
}
