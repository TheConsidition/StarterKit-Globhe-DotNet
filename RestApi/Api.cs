using System;
using TheConsidition.Globhe.RestApi.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Threading;

namespace RestApi
{
    /**
     * <summary>Wrapper class to communicate with The Considition 2019 API</summary>
     */
    public static class Api
    {
        private const string BasePath = "https://api.theconsidition.se/api/game";
        private static readonly RestClient Client = new RestClient(BasePath);

        /**
         * <summary>Closes any active game and starts a new one, returning an object with information about the game</summary>
         */
        public static GameSettingsInfo InitGame(string apiKey)
        {
            var request = new RestRequest("/init", Method.GET);
            request.AddHeader("x-api-key", apiKey);
            var response = Client.Execute(request);
            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Fatal Error: could not start a new game");
                PrintResponseError(response);
                Environment.Exit(1);
            }
            GameSettingsInfo result = JsonConvert.DeserializeObject<GameSettingsInfo>(response.Content);
            return result;

        }
        /**
         * <summary>
         * Gets a zip-file containing images for the current round and returns a bytearray representing the file.
         * Tries fetching the zip file three times
         * </summary>
         */
        public static byte[] GetImages(string apiKey) 
        {
            var request = new RestRequest("/images", Method.GET);
            request.AddHeader("x-api-key", apiKey);
            var tries = 1;
            while(tries <= 3)
            {
                var response = Client.Execute(request);
                Console.WriteLine($"Fetching images...");
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return response.RawBytes;
                }
                PrintResponseError(response);
                Console.WriteLine($"Attempt {tries} failed, waiting 2 sec and attempting again.");
                Thread.Sleep(2000);
                tries++;
            }

            Console.WriteLine("Fatal Error: could not fetch images");
            Environment.Exit(1);
            return null;
        }

        /**
         * <summary>
         * Posts the solution for evaluation. Returns a summary of the score and game state.
         * Tries to submit the solution three times
         * </summary>
         */
        public static ScoreResponse ScoreSolution(string apiKey, ScoreSolutionRequest solution)
        {

            var request = new RestRequest("/solution", Method.POST);
            request.AddHeader("x-api-key", apiKey);
            request.AddJsonBody(solution);
            var tries = 1;
            while (tries <= 3)
            {
                var response = Client.Execute(request);
                Console.WriteLine($"Scoring Solution...");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<ScoreResponse>(response.Content);
                }
                PrintResponseError(response);
                Console.WriteLine($"Attempt {tries} failed, waiting 2 sec and attempting again.");
                Thread.Sleep(2000);
                tries++;
            }
            Console.WriteLine("Fatal Error: could submit solution");
            Environment.Exit(1);
            return null;
        }

        private static void PrintResponseError(IRestResponse response)
        {
            if (response.ErrorException != null)
            {
                Console.WriteLine($"Error: {response.ErrorMessage}");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}: {response.Content}");
            }
        }
    }
}
