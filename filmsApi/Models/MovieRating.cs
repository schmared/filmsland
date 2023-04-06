namespace filmsApi.Models;

public interface IMovieRating
{
    int Id { get; set; }
    ParentalGuide Rating { get; set; }
}

public class MovieRating : IMovieRating
{
    public int Id { get; set; }
    public ParentalGuide Rating { get; set; }
}