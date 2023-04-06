using filmsApi.Services;
using Microsoft.AspNetCore.Mvc;
using filmsApi.Models.Parameters;

namespace filmsApi.Controllers;

[Route("movierating")]
public class MovieRatingGetController : GetController<MovieRatingSearchParameters>
{
    private readonly IMovieRatingService _movieRatingService;
    public MovieRatingGetController(IMovieRatingService movieRatingService) => _movieRatingService = movieRatingService;

    /// <summary>
    /// Retrieves the movie rating entity from the movie rating service by the specified Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>The movie rating entity from the movie rating service</returns>
    [HttpGet]
    [Route("{id:int}")]
    public override IActionResult Get(int id) => Ok(_movieRatingService.Read(id));

    /// <summary>
    /// Retrieves the movie rating entities from the movie rating service by the specified search parameters
    /// </summary>
    /// <param name="searchParameters"></param>
    /// <returns>The movie rating entities found by the movie rating service</returns>
    [HttpGet]
    public override IActionResult List([FromQuery]MovieRatingSearchParameters searchParameters) => Ok(_movieRatingService.ReadList(searchParameters));
}