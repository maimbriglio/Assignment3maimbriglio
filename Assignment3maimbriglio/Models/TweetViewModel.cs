namespace Assignment3maimbriglio.Models
{
    public class TweetViewModel
    {
        public int Id { get; set; }
        public Actor actor { get; set; }
        public List<TweetWithScore> Tweets { get; set; }
        public double AvgCompoundScore() 
        {
            if (Tweets == null) return 0;
            int valid = 0;
            double total = 0;
            foreach(TweetWithScore tweet in Tweets) 
            {
                if(tweet.Score !=0) 
                {
                    total += tweet.Score;
                    valid++;
                }
            }
            return total / valid;
        }
    }
}
