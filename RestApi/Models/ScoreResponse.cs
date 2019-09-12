using System.Collections.Generic;           


namespace TheConsidition.Globhe.RestApi.Models
{
    public class ScoreResponse
    {
        public int TotalScore { get; set; }
        public List<ImageScoreModel> ImageScores { get; set; }
        public int RoundsLeft { get; set; }
        public List<string> Errors { get; set; }
    }

    
   
    public class ImageScoreModel
    {
        public string ImageName { get; set; }
        public int Score { get; set; }
    }

    public class ImageScoreResponse
    {
        public string ImageName { get; set; }
        public int Score { get; set; }

        public ImageScoreResponse(ImageScoreModel imageScore)
        {
            ImageName = imageScore.ImageName;
            Score = imageScore.Score;
        }

        public override string ToString()
        {
            return ImageName + ": " + Score;
        }
    }
}
