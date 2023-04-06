using System.Runtime.CompilerServices;
using filmsApi.DataAccess;
using filmsApi.Models;
using filmsApi.Models.Parameters;
using Microsoft.EntityFrameworkCore;

namespace filmsApi.Services;

public interface IMovieService : IQueryService<Movie, MovieSearchParameters> {}
public class MovieService : IMovieService
{
    private readonly FilmsContext _context;
    public MovieService(FilmsContext context) => _context = context;

    /// <summary>
    /// Retrieve record with exact matching identitifier field
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Record if found with the matching identifier</returns>
    public Movie? Read(int id) => _context.Movies?.Include(m => m.Actors).Where(m => m.Id == id).FirstOrDefault();

    /// <summary>
    /// Retrieve records matching provided search criteria
    /// </summary>
    /// <param name="movieSearchParameters"></param>
    /// <returns>List of records matching the provided search criteria</returns>
    public IList<Movie> ReadList(MovieSearchParameters movieSearchParameters)
    {
        var query = _context.Movies?.Include(m => m.Actors).AsQueryable();

        if(query == null)
            return new List<Movie>();

        var filteredQuery = QueryConditionalSearchParameters(query, movieSearchParameters)
                .OrderBy(Movie => Movie.Id);

        if (filteredQuery != null && movieSearchParameters.Page != null && movieSearchParameters.PageSize != null)
            return filteredQuery
                .Skip(movieSearchParameters.Page.Value * movieSearchParameters.PageSize.Value)
                .Take(movieSearchParameters.PageSize.Value)
                .ToList();

        return filteredQuery?.ToList() ?? new List<Movie>();
    }

    /// <summary>
    /// Creates a new record in the database
    /// </summary>
    /// <param name="movie"></param>
    /// <returns>The newly created record if successful</returns>
    public Movie? Create(Movie movie)
    {
        // See if record already exists
        var record = Read(movie.Id);
        if (record != null)
            return movie;

        // Insert the new record into the database
        var actors = new List<Actor>();
        foreach (var actor in movie.Actors)
        {
            actors.Add(new Actor
            {
                Id = actor.Id,
                Name = actor.Name,
                YearOfBirth = actor.YearOfBirth
            });
        }

        var data = new Movie
        {
            Id = movie.Id,
            Title = movie.Title,
            Rating = movie.Rating,
            Released = movie.Released,
            Length = movie.Length,
            Actors = actors
        };

        _context.Movies?.Add(data);
        _context.SaveChanges();

        //Get the newly set SCOPE_IDENTITY value
        int id = movie.Id;

        //retrieve the newly created object from the database to return
        return Read(id);
    }

    /// <summary>
    /// Modifies an existing record in the database
    /// </summary>
    /// <param name="movie"></param>
    /// <returns>The modified record if successful</returns>
    public Movie? Update(Movie movie)
    {
        // Ensure record already exists
        var record = Read(movie.Id);
        if (record == null)
            return null;

        // Update the record in the database
        record.Title = movie.Title;
        record.Rating = movie.Rating;
        record.Released = movie.Released;
        record.Length = movie.Length;

        _context.SaveChanges();

        // Return updated object from the databaase
        return Read(movie.Id);
    }

    /// <summary>
    /// Deletes a specified record in the database that matches the provided identifier
    /// </summary>
    /// <param name="id"></param>
    /// <returns>True if record is deleted, false if unsuccessful or record does not exist</returns>
    public bool Delete(int id)
    {
        // See if record exists
        var movie = Read(id);

        // If not say it's deleted (return true)
        if (movie == null)
            return true;

        // Else remove the record
        _context.Remove(movie);
        _context.SaveChanges();

        // If there are not any records left or it is no longer found then say it's deleted (return true)
        return _context.Movies?.All(m => m.Id != id) ?? true;
    }

    /// <summary>
    /// Bulids a list of conditional 'where clause' conditions into an IQueryable
    /// </summary>
    /// <param name="query"></param>
    /// <param name="movieSearchParameters"></param>
    /// <returns>The modified IQueryable object</returns>
    public IQueryable<Movie> QueryConditionalSearchParameters(IQueryable<Movie> query, MovieSearchParameters movieSearchParameters)
    {
        return query

        //Search if Movie Title contains search string
        .If(movieSearchParameters.TitlePart != null,
            query => query.Where(movie => movie.Title.Contains(movieSearchParameters.TitlePart ?? string.Empty)))

        //Search for exact match Parental Guide ratings
        .If(movieSearchParameters.ParentalGuide != null,
            query => query.Where(m => m.Rating.Rating == movieSearchParameters.ParentalGuide))

        // Search for Movie release date after requested year
        .If(movieSearchParameters.Released != null && movieSearchParameters.ReleasedAfter != null && movieSearchParameters.Released.Value > 0 && movieSearchParameters.ReleasedAfter.Value,
            query => query.Where(m => m.Released >= movieSearchParameters.Released))
        
        // Search for Movie release date before requested year
        .If(movieSearchParameters.Released != null && movieSearchParameters.ReleasedAfter != null && movieSearchParameters.Released.Value > 0 && !movieSearchParameters.ReleasedAfter.Value,
            query => query.Where(m => m.Released <= movieSearchParameters.Released))
        
        // Search for Movies shorter than requested length
        .If(movieSearchParameters.Length != null && movieSearchParameters.LengthLessThan != null && movieSearchParameters.Length.Value != 0 && movieSearchParameters.LengthLessThan.Value,
            query => query.Where(m => m.Length <= movieSearchParameters.Length))
        
        // Search for Movies longer than requested length
        .If(movieSearchParameters.Length != null && movieSearchParameters.LengthLessThan != null && movieSearchParameters.Length.Value != 0 && !movieSearchParameters.LengthLessThan.Value,
            query => query.Where(m => m.Length >= movieSearchParameters.Length))
        
        // Search for Movies that contain a specific ActorId
        .If(movieSearchParameters.Actor != null,
            query => query.Where(m => m.Actors.Select(a => a.Name).ToList().Contains(movieSearchParameters.Actor ?? string.Empty)));
    }

    internal static string TimeOnlyToDBString(TimeOnly timeOnly) =>
        string.Format("{0}:{1}:{2}", timeOnly.Hour.ToString(), timeOnly.Minute.ToString(), timeOnly.Second.ToString());

    internal static TimeOnly DBStringToTimeOnly(string dbString)
    {
        var parts = dbString.Split(':', StringSplitOptions.RemoveEmptyEntries);
        return new TimeOnly(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
    }
}