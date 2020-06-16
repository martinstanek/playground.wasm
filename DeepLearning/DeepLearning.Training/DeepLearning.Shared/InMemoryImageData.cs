namespace DeepLearning.Shared
{
    public class InMemoryImageData
    {
        public InMemoryImageData(byte[] image, string label, string imageFileName)
        {
            Image = image;
            Label = label;
            ImageFileName = imageFileName;
        }

        public byte[] Image { get; }

        public string Label { get; }

        public string ImageFileName { get; }
    }
}
