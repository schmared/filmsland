using filmsApi.Services;
using filmsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace filmsApi.Controllers.CreateUpdateDelete;

[Route("movie")]
public class MovieCreateUpdateDeleteController : CreateUpdateDeleteController<Movie>
{
    private readonly IMovieService _movieService;
    public MovieCreateUpdateDeleteController(IMovieService movieService) => _movieService = movieService;

    /// <summary>
    /// Creates a new movie entity by the movie service using the movie object provided
    /// </summary>
    /// <param name="movie"></param>
    /// <returns>The movie object successfully added by the movie service</returns>
    [HttpPost]
    public override IActionResult Create([FromBody]Movie movie)
    {
        // if no movie object provided then Create cannot occur
        if (string.Compare(movie.Title, "Unknown") == 0)
            return BadRequest("Please provide a new movie in the request body");

        try
        {
            return Ok(_movieService.Create(movie));
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    /// <summary>
    /// Updates an existing entity by the movie service using the movie object provided
    /// </summary>
    /// <param name="movie"></param>
    /// <returns>The movie object successfully updated by the movie service</returns>
    [HttpPut]
    public override IActionResult Update([FromBody]Movie movie)
    {
        // if no movie object provided then Update cannot occur
        if (string.Compare(movie.Title, "Unknown") == 0)
            return BadRequest("Please provide a movie to update in the request body");

        var movieRecord = _movieService.Update(movie);
        return movieRecord == null ? NotFound() : Ok(movieRecord);
    }

    /// <summary>
    /// Attempts to delete movie entity by the movie service and the Id provided
    /// </summary>
    /// <param name="id">Id of the movie database entity to be deleted</param>
    /// <returns>Ok result if entity found and deleted or NotFound result if no entity was found or deleted</returns>
    [HttpDelete("{id}")]
    public override IActionResult Delete(int id) => _movieService.Delete(id) ? Ok() : NotFound();
}