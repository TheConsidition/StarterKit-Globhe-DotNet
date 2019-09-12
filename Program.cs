using TheConsidition.Globhe.StarterKit.RestApi;
using RestApi;
using System;
using System.Collections.Generic;
using TheConsidition.Globhe.RestApi.Models;

namespace TheConsidition.Globhe.StarterKit
{
    public class Program
    {
        private static readonly string _apiKey = "YOUR-API-KEY-HERE";                   // TODO: Enter your API key 
        private static readonly string _imageFolderPath = "path/to/imageFolder";        // TODO: Enter the path to your Image Folder 

        public static void Main(string[] args)
        {
            GameSettingsInfo result = Api.InitGame(_apiKey);
            string gameId = result.GameId;
            int roundsLeft = result.NumberOfRounds;
            Console.WriteLine($"Starting a new game with id: {gameId}");
            Console.WriteLine($"The game has {roundsLeft} rounds and {result.ImagesPerRound} images per round");
            while (roundsLeft > 0)
            {
                Console.WriteLine($"Starting new round, {roundsLeft} rounds left");
                var solution = new Solution();
                var zip = Api.GetImages(_apiKey);
                List<string> imageNames = SolutionHelper.SaveImagesToDisk(zip, _imageFolderPath);
                foreach (string name in imageNames)
                {
                    var imagePath = _imageFolderPath + "/" + name;
                    var imageSolution = ImageAnalyzer(imagePath);
                    solution.Add(name, imageSolution);
                }
                ScoreResponse response = Api.ScoreSolution(_apiKey, solution.GetRequest());
                SolutionHelper.PrintErrors(response);
                SolutionHelper.PrintScores(response);
                roundsLeft = response.RoundsLeft;
            }
            SolutionHelper.ClearImagesFromFolder(_imageFolderPath);
        }


        public static ImageSolution ImageAnalyzer(string imagePath)
        {
            /**
             * ----------------------------------------------------
             * TODO Implement your image recognition algorithm here
             * ----------------------------------------------------
             */

            ImageSolution answer = new ImageSolution
            {
                BuildingPercentage = 30.0m,
                WaterPercentage = 30.0m,
                RoadPercentage = 30.0m,
            };
            return answer;
        }
    }
}
