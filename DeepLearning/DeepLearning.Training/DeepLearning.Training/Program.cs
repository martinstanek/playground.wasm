using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;

using static Microsoft.ML.Transforms.ValueToKeyMappingEstimator;

namespace DeepLearning.Training
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Let's learn some fruits!");

            var assetsPath = GetAbsolutePath(@"../../../Assets");
            var outputMlNetModelFilePath = Path.Combine(assetsPath, "Output", "fruitImageClassifier.zip");
            var testImagesFolderPath = Path.Combine(assetsPath, "Test");
            var inputImagesFolderPath = Path.Combine(assetsPath, "Input");

            var mlContext = new MLContext(seed: 1);

            mlContext.Log += MlContextLog;

            // Load the initial full image-set into an IDataView and shuffle, so it'll be better balanced
            var images = LoadImagesFromDirectory(inputImagesFolderPath);
            var fullImagesDataset = mlContext.Data.LoadFromEnumerable(images);
            var shuffledFullImageFilePathsDataset = mlContext.Data.ShuffleRows(fullImagesDataset);

            // Load Images with in-memory type within the IDataView and Transform Labels to Keys (Categorical)
            var shuffledFullImagesDataset = mlContext.Transforms.Conversion
                .MapValueToKey(outputColumnName: "LabelAsKey", inputColumnName: "Label", keyOrdinality: KeyOrdinality.ByValue)
                .Append(mlContext.Transforms.LoadRawImageBytes("Image", inputImagesFolderPath, "ImagePath"))
                .Fit(shuffledFullImageFilePathsDataset)
                .Transform(shuffledFullImageFilePathsDataset);

            // Split the data 80:20 into train and test sets, train and evaluate.
            var trainTestData = mlContext.Data.TrainTestSplit(shuffledFullImagesDataset, testFraction: 0.2);
            var trainDataView = trainTestData.TrainSet;
            var testDataView = trainTestData.TestSet;

            // Define the model's training pipeline using DNN default values
            var pipeline = mlContext.MulticlassClassification.Trainers
                .ImageClassification(featureColumnName: "Image", labelColumnName: "LabelAsKey", validationSet: testDataView)
                .Append(mlContext.Transforms.Conversion
                    .MapKeyToValue(outputColumnName: "PredictedLabel", inputColumnName: "PredictedLabel"));

            // Train
            var trainedModel = pipeline.Fit(trainDataView);

            // Get the quality metrics (accuracy, etc.)
            EvaluateModel(mlContext, testDataView, trainedModel);

            // Save the model to assets/outputs (You get ML.NET .zip model file and TensorFlow .pb model file)
            SaveModel(mlContext, trainedModel, trainDataView, outputMlNetModelFilePath);

            TestModel(testImagesFolderPath, mlContext, trainedModel);

            Console.ReadLine();
        }

        private static void TestModel(string imagesFolderPathForPredictions, MLContext mlContext, ITransformer trainedModel)
        {
            var predictionEngine = mlContext.Model.CreatePredictionEngine<InMemoryImageData, ImagePrediction>(trainedModel);

            var testImages = LoadInMemoryImagesFromDirectory(imagesFolderPathForPredictions);

            foreach (var testImage in testImages)
            {
                var prediction = predictionEngine.Predict(testImage);

                Console.WriteLine(
                    $"Image Filename : [{testImage.ImageFileName}], " +
                    $"Scores : [{string.Join(",", prediction.Score)}], " +
                    $"Predicted Label : {prediction.PredictedLabel}");
            }
        }

        private static void EvaluateModel(MLContext mlContext, IDataView testDataset, ITransformer trainedModel)
        {
            Console.WriteLine("Making predictions in bulk for evaluating model's quality ...");

            var predictionsDataView = trainedModel.Transform(testDataset);

            var metrics = mlContext.MulticlassClassification
                .Evaluate(predictionsDataView, labelColumnName: "LabelAsKey", predictedLabelColumnName: "PredictedLabel");

            PrintMultiClassClassificationMetrics("TensorFlow DNN Transfer Learning", metrics);
        }

        private static void SaveModel(MLContext mlContext, ITransformer trainedModel, IDataView trainDataView, string outputMlNetModelFilePath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(outputMlNetModelFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outputMlNetModelFilePath));
            }

            mlContext.Model.Save(trainedModel, trainDataView.Schema, outputMlNetModelFilePath);
        }

        public static void PrintMultiClassClassificationMetrics(string name, MulticlassClassificationMetrics metrics)
        {
            Console.WriteLine($"************************************************************");
            Console.WriteLine($"*    Metrics for {name} multi-class classification model   ");
            Console.WriteLine($"*-----------------------------------------------------------");
            Console.WriteLine($"    AccuracyMacro = {metrics.MacroAccuracy:0.####}, a value between 0 and 1, the closer to 1, the better");
            Console.WriteLine($"    AccuracyMicro = {metrics.MicroAccuracy:0.####}, a value between 0 and 1, the closer to 1, the better");
            Console.WriteLine($"    LogLoss = {metrics.LogLoss:0.####}, the closer to 0, the better");

            int i = 0;
            foreach (var classLogLoss in metrics.PerClassLogLoss)
            {
                i++;
                Console.WriteLine($"    LogLoss for class {i} = {classLogLoss:0.####}, the closer to 0, the better");
            }
            Console.WriteLine($"************************************************************");
        }

        private static IEnumerable<ImageData> LoadImagesFromDirectory(string path)
        {
            var files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);

            return files.Select(s => new ImageData(s, Directory.GetParent(s).Name));
        }

        public static IEnumerable<InMemoryImageData> LoadInMemoryImagesFromDirectory(string path)
        {
            var files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);

            return files.Select(s => new InMemoryImageData(File.ReadAllBytes(s), string.Empty, Path.GetFileName(s)));
        }

        private static void MlContextLog(object sender, LoggingEventArgs e)
        {
            if (e.Message.StartsWith("[Source=ImageClassificationTrainer;"))
            {
                Console.WriteLine(e.Message);
            }
        }

        private static string GetAbsolutePath(string relativePath)
        {
            var assemblyFolderPath = new FileInfo(typeof(Program).Assembly.Location).Directory.FullName;

            return Path.Combine(assemblyFolderPath, relativePath);
        }
    }
}
