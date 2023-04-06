using filmsApi.Services;
using filmsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace filmsApi.Controllers;

[Route("actor")]
public class ActorCreateUpdateDeleteController : CreateUpdateDeleteController<Actor>
{
    private readonly IActorService _actorService;
    public ActorCreateUpdateDeleteController(IActorService actorService) => _actorService = actorService;

    /// <summary>
    /// Creates a new actor entity by the actor service using the actor object provided
    /// </summary>
    /// <param name="actor"></param>
    /// <returns>The actor object successfully added by the actor service</returns>
    [HttpPost]
    public override IActionResult Create([FromBody]Actor actor)
    {
        // if no actor object provided then Create cannot occur
        if (actor.Name == string.Empty)
            return BadRequest("Please provide a new actor in the request body");

        try
        {
            return Ok(_actorService.Create(actor));
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    /// <summary>
    /// Updates an existing entity by the actor service using the actor object provided
    /// </summary>
    /// <param name="actor"></param>
    /// <returns>The actor object successfully updated by the actor service</returns>
    [HttpPut]
    public override IActionResult Update([FromBody]Actor actor)
    {
        // if no actor object provided then Update cannot occur
        if (actor.Name == string.Empty)
            return BadRequest("Please provide an actor to update in the request body");

        var actorRecord = _actorService.Update(actor);
        return actorRecord == null ? NotFound() : Ok(actorRecord);
    }

    /// <summary>
    /// Attempts to delete actor entity by the actor service and the Id provided
    /// </summary>
    /// <param name="id">Id of the actor database entity to be deleted</param>
    /// <returns>Ok result if entity found and deleted or NotFound result if no entity was found or deleted</returns>
    [HttpDelete("{id}")]
    public override IActionResult Delete(int id) => _actorService.Delete(id) ? Ok() : NotFound();
}