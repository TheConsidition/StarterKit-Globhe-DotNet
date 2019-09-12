using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheConsidition.Globhe.RestApi.Models;

namespace TheConsidition.Globhe.StarterKit.RestApi
{
    /**
     * <summary>Helper class to incrementally build a solution for a round of images</summary>
     */
    public class Solution
    {
        ScoreSolutionRequest request;
        public Solution()
        {
            request = new ScoreSolutionRequest();
            request.Solutions = new List<ImageSolutionRequest>();
        }

        /**
         * <summary>Adds a solution for a single image to the total solution</summary>
         */
        public void Add(string name, ImageSolution imageSolution)
        {
            request.Solutions.Add(new ImageSolutionRequest
            {
                ImageName = name,
                BuildingPercentage = imageSolution.BuildingPercentage,
                RoadPercentage = imageSolution.RoadPercentage,
                WaterPercentage = imageSolution.WaterPercentage
            });
        }

        /**
         * <summary>Returns a request for this solution</summary>
         */
        public ScoreSolutionRequest GetRequest()
        {
            return request;
        }
    }
}
