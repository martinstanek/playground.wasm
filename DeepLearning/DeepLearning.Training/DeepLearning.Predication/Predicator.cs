using System.IO;
using DeepLearning.Shared;
using Microsoft.ML;

namespace DeepLearning.Predication
{
    public class Predicator
    {
        const string modelPath = "/Users/martinstanek/Documents/Developing/Temp/playground.wasm/DeepLearning/DeepLearning.Training/DeepLearning.Training/Assets/Output/fruitImageClassifier.zip";

        public string WhatIsOnImage(string path)
        {
            var mlContext = new MLContext();

            var trainedModel = mlContext.Model.Load(modelPath, out _);

            var predictionEngine = mlContext.Model.CreatePredictionEngine<InMemoryImageData, ImagePrediction>(trainedModel);

            var testImage = new InMemoryImageData(File.ReadAllBytes(path), "", Path.GetFileName(path));

            var prediction = predictionEngine.Predict(testImage);

            return prediction.PredictedLabel;
        }
    }
}
