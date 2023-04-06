using filmsApi.Services;
using filmsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace filmsApi.Controllers;

[Route("movierating")]
public class MovieRatingCreateUpdateDeleteController : CreateUpdateDeleteController<MovieRating>
{
    private readonly IMovieRatingService _movieRatingService;
    public MovieRatingCreateUpdateDeleteController(IMovieRatingService movieRatingService) => _movieRatingService = movieRatingService;

    /// <summary>
    /// Creates a new movie rating entity by the movie rating service using the movie rating object provided
    /// </summary>
    /// <param name="movieRating" ></param>
    /// <returns>The movie rating object successfully added by the movie rating service</returns>
    [HttpPost]
    public override IActionResult Create([FromBody]MovieRating movieRating)
    {
        // if no movie rating object provided then Create cannot occur
        if (movieRating.Id == 0)
            return BadRequest("Please provide a new rating with Id in the request body");

        return Ok(_movieRatingService.Create(movieRating));
    }

    /// <summary>
    /// Updates an existing entity by the movie rating service using the movie rating object provided
    /// </summary>
    /// <param name="movieRating" ></param>
    /// <returns>The movie rating object successfully updated by the movie rating service</returns>
    [HttpPut]
    public override IActionResult Update([FromBody]MovieRating movieRating)
    {
        // if no movie rating object provided then Update cannot occur
        if (movieRating.Id == 0)
            return BadRequest("Please provide a rating to update in the request body");

        var movieRatingRecord = _movieRatingService.Update(movieRating);
        return movieRatingRecord == null ? NotFound() : Ok(movieRatingRecord);
    }

    /// <summary>
    /// Attempts to delete movie rating entity by the movie rating service and the Id provided
    /// </summary>
    /// <param name="id">Id of the movie rating database entity to be deleted</param>
    /// <returns>Ok result if entity found and deleted or NotFound result if no entity was found or deleted</returns>
    [HttpDelete("{id}")]
    public override IActionResult Delete(int id) => _movieRatingService.Delete(id) ? Ok() : NotFound();
}