using System;
using DeepLearning.Predication;

namespace DeepLearning.Prediction.Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (!string.IsNullOrWhiteSpace(args[0]) && !string.IsNullOrWhiteSpace(args[1]))
            {
                var predicator = new Predicator(args[1]);

                Console.WriteLine(predicator.WhatIsOnImage(args[0]));
            }
        }
    }
}
