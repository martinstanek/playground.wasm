namespace DeepLearning.Training
{
    public class ImageData
    {
        public ImageData(string imagePath, string label)
        {
            ImagePath = imagePath;
            Label = label;
        }

        public string ImagePath { get; }

        public string Label { get; }
    }
}