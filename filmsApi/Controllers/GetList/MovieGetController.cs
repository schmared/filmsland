using filmsApi.Services;
using Microsoft.AspNetCore.Mvc;
using filmsApi.Models.Parameters;

namespace filmsApi.Controllers;

[Route("movie")]
public class MovieGetController : GetController<MovieSearchParameters>
{
    private readonly IMovieService _movieService;
    public MovieGetController(IMovieService movieService) => _movieService = movieService;

    /// <summary>
    /// Retrieves the movie entity from the movie service by the specified Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>The movie entity from the movie service</returns>
    [HttpGet]
    [Route("{id:int}")]
    public override IActionResult Get(int id) => Ok(_movieService.Read(id));

    /// <summary>
    /// Retrieves the movie entities from the movie service by the specified search parameters
    /// </summary>
    /// <param name="searchParameters"></param>
    /// <returns>The movie entities found by the movie service</returns>
    [HttpGet]
    public override IActionResult List([FromQuery]MovieSearchParameters searchParameters) => Ok(_movieService.ReadList(searchParameters));
}