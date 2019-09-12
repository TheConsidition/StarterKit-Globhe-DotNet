using System;
using System.Collections.Generic;

namespace TheConsidition.Globhe.RestApi.Models
{
    public class ScoreSolutionRequest
    {
        public List<ImageSolutionRequest> Solutions { get; set; }
    }

    public class ImageSolutionRequest
    {
        public string ImageName { get; set; }
        public decimal BuildingPercentage { get; set; }
        public decimal RoadPercentage { get; set; }
        public decimal WaterPercentage { get; set; }
    }
}