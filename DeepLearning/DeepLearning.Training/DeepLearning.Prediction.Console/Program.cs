using System;
using DeepLearning.Predication;

namespace DeepLearning.Prediction.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var predicator = new Predicator();

            if (!string.IsNullOrWhiteSpace(args[0]))
            {
                Console.WriteLine(predicator.WhatIsOnImage(args[0]));
            }

            Console.ReadLine();
        }
    }
}
