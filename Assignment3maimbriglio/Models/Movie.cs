using System.Reflection.Metadata;

namespace Assignment3maimbriglio.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string IMDB { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public byte[]? Photo { get; set; }


    }
}
