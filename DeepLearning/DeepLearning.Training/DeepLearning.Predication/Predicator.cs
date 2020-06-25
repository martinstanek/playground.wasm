using System.IO;
using DeepLearning.Shared;
using Microsoft.ML;

namespace DeepLearning.Predication
{
    public class Predicator
    {
        private readonly string _modelPath;

        public Predicator(string modelPath)
        {
            _modelPath = modelPath;
        }

        public string WhatIsOnImage(string path)
        {
            return WhatIsOnImage(File.ReadAllBytes(path), Path.GetFileName(path));
        }

        public string WhatIsOnImage(byte[] rawData, string imageName)
        {
            var mlContext = new MLContext();

            mlContext.Log += (object sender, LoggingEventArgs e) => { };

            var trainedModel = mlContext.Model.Load(_modelPath, out _);

            var predictionEngine = mlContext.Model.CreatePredictionEngine<InMemoryImageData, ImagePrediction>(trainedModel);

            var testImage = new InMemoryImageData(rawData, "", imageName);

            var prediction = predictionEngine.Predict(testImage);

            return prediction.PredictedLabel;
        }
    }
}
