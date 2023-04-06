using filmsApi.DataAccess;
using filmsApi.Models;
using filmsApi.Models.Parameters;

namespace filmsApi.Services;

public interface IMovieRatingService : IQueryService<MovieRating, MovieRatingSearchParameters> {}
public class MovieRatingService : IMovieRatingService
{
    private readonly FilmsContext _context;
    public MovieRatingService(FilmsContext context) => _context = context;

    /// <summary>
    /// Retrieve record with exact matching identitifier field
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Record if found with the matching identifier</returns>
    public MovieRating? Read(int id) => _context.MovieRatings?.Where(m => m.Id == id).FirstOrDefault();

    /// <summary>
    /// Retrieve records matching provided search criteria
    /// </summary>
    /// <param name="movieRatingSearchParameters"></param>
    /// <returns>List of records matching the provided search criteria</returns>
    public IList<MovieRating> ReadList(MovieRatingSearchParameters movieRatingSearchParameters)
    {
        var query = _context.MovieRatings?.AsQueryable();

        if(query == null)
            return new List<MovieRating>();

        var results = QueryConditionalSearchParameters(query, movieRatingSearchParameters)
                .OrderBy(MovieRating => MovieRating.Id);

        if (results != null && movieRatingSearchParameters.Page != null && movieRatingSearchParameters.PageSize != null)
            return results
                .Skip(movieRatingSearchParameters.Page.Value * movieRatingSearchParameters.PageSize.Value)
                .Take(movieRatingSearchParameters.PageSize.Value)
                .ToList();
                
        return results?.ToList() ?? new List<MovieRating>();
    }

    /// <summary>
    /// Creates a new record in the database
    /// </summary>
    /// <param name="movieRating"></param>
    /// <returns>The newly created record if successful</returns>
    public MovieRating? Create(MovieRating movieRating)
    {
        // See if record already exists
        if (Read(movieRating.Id) != null)
            return movieRating;

        // Insert the new record into the database
        _context.Add(movieRating);
        _context.SaveChanges();

        //Get the newly set SCOPE_IDENTITY value
        int id = movieRating.Id;

        //retrieve the newly created object from the database to return
        return Read(id);
    }

    /// <summary>
    /// Modifies an existing record in the database
    /// </summary>
    /// <param name="movieRating"></param>
    /// <returns>The modified record if successful</returns>
    public MovieRating? Update(MovieRating movieRating)
    {
        // Ensure record already exists
        var record = Read(movieRating.Id);
        if (record == null)
            return null;

        // Update the record in the database
        record.Rating = movieRating.Rating;
        _context.SaveChanges();

        // Return updated object from the databaase
        return Read(movieRating.Id);
    }

    /// <summary>
    /// Deletes a specified record in the database that matches the provided identifier
    /// </summary>
    /// <param name="id"></param>
    /// <returns>True if record is deleted, false if unsuccessful or record does not exist</returns>
    public bool Delete(int id)
    {
        // See if record exists
        var movieRating = Read(id);

        // If not say it's deleted (return true)
        if (movieRating == null)
            return true;

        // Else remove the record
        _context.Remove(movieRating);
        _context.SaveChanges();

        // If there are not any records left or it is no longer found then say it's deleted (return true)
        return _context.MovieRatings?.All(m => m.Id != id) ?? true;
    }

    /// <summary>
    /// Bulids a list of conditional 'where clause' conditions into an IQueryable
    /// </summary>
    /// <param name="query"></param>
    /// <param name="movieRatingSearchParameters"></param>
    /// <returns>The modified IQueryable object</returns>
    public IQueryable<MovieRating> QueryConditionalSearchParameters(IQueryable<MovieRating> query, MovieRatingSearchParameters movieRatingSearchParameters)
    {
        return query

        //Search for exact match Parental Guide ratings
        .If(movieRatingSearchParameters.Rating != null,
            query => query.Where(m => m.Rating == movieRatingSearchParameters.Rating))

        // Search for Parental Guide ratings that are not child ratings
        .If(movieRatingSearchParameters.NoChildRatings != null && movieRatingSearchParameters.NoChildRatings.Value,
            query => query.Where(m => GetAdultRatings.Contains(m.Rating)))
        
        // Search for Parental Guide ratings that are not adult ratings
        .If(movieRatingSearchParameters.NoAdultRatings != null && movieRatingSearchParameters.NoAdultRatings.Value,
            query => query.Where(m => GetChildRatings.Contains(m.Rating)));
    }

    // Gets Parental Guide Ratings from the enum which are PG and less (such as G)
    private static IEnumerable<ParentalGuide> GetChildRatings
        { get => Enum.GetValues(typeof(ParentalGuide)).Cast<ParentalGuide>().Where(p => p <= ParentalGuide.PG); }
    // Gets Parental Guide Ratings from the enum which are PG13 and more (such as R or NC17)
    private static IEnumerable<ParentalGuide> GetAdultRatings
        { get => Enum.GetValues(typeof(ParentalGuide)).Cast<ParentalGuide>().Where(p => p >= ParentalGuide.PG13); }
}
