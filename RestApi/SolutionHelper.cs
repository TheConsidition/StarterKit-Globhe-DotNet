using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using TheConsidition.Globhe.RestApi.Models;

namespace TheConsidition.Globhe.StarterKit.RestApi
{
    public static class SolutionHelper
    {
        /**
         * <summary>Opens a zip file and stores the images to the specified path. Returns a list of names of the images</summary>
         */
        public static List<string> SaveImagesToDisk(byte[] zip, string imageFolderPath)
        {
            List<string> imageNames = new List<string>();
            if (!Directory.Exists(imageFolderPath)) Directory.CreateDirectory(imageFolderPath);
            using (var ms = new MemoryStream(zip))
            {
                using (var zipArchive = new ZipArchive(ms, ZipArchiveMode.Read))
                {
                    foreach (var entry in zipArchive.Entries)
                    {
                        imageNames.Add(entry.FullName);
                        var image = entry.Open();
                        using (var fileStream = new FileStream(imageFolderPath + "/" + entry.FullName, FileMode.Create))
                        {
                            image.CopyTo(fileStream);
                        }
                    }
                }
            }

            return imageNames;
        }

        /**
         * <summary>Clears the specified folder of all .jpg images</summary>
         */
        public static void ClearImagesFromFolder(string imageFolderPath)
        {
            foreach (var file in Directory.GetFiles(imageFolderPath))
            {
                if(file.Contains(".jpg"))
                {
                    System.IO.File.Delete(file);
                }
            }
        }

        /**
         * <summary>Prints any errors encountered in the solution</summary>
         */
        public static void PrintErrors(ScoreResponse response)
        {
            if (response.Errors == null || response.Errors.Count == 0) return;

            Console.WriteLine("Encountered some errors with the solution:");
            foreach (string error in response.Errors)
            {
                Console.WriteLine(error);
            }
        }

        /**
         * <summary>Prints the scores for this round</summary>
         */
        public static void PrintScores(ScoreResponse response)
        {
            Console.WriteLine($"Total score: {response.TotalScore}");
            foreach (var imageScore in response.ImageScores)
            {
                Console.WriteLine($"Image {imageScore.ImageName,25} got a score of {imageScore.Score}");
            }
        }
    }
}
