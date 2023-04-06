namespace filmsApi.Models
{
    public interface IMovie
    {
        int Id { get; set; }
        string Title { get; set; }
        MovieRating Rating { get; set; }
        int Released { get; set; }
        int Length { get; set; }
    }

    public class Movie : IMovie
    {
        public int Id { get; set; }
        public string Title { get; set; } = "Unknown";
        public MovieRating Rating { get; set; } = new MovieRating();
        public int Released { get; set; } = 2023;
        public int Length { get; set; } = 0;
        public IList<Actor> Actors { get; set; } = new List<Actor>();
    }
}