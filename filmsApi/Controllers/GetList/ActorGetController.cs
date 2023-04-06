using filmsApi.Models.Parameters;
using filmsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace filmsApi.Controllers;

[Route("actor")]
public class ActorGetController : GetController<ActorSearchParameters>
{
    private readonly IActorService _actorService;
    public ActorGetController(IActorService actorService) => _actorService = actorService;

    /// <summary>
    /// Retrieves the actor entity from the actor service by the specified Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>The actor entity from the actor service</returns>
    [HttpGet]
    [Route("{id:int}")]
    public override IActionResult Get(int id) => Ok(_actorService.Read(id));

    /// <summary>
    /// Retrieves the actor entities from the actor service by the specified search parameters
    /// </summary>
    /// <param name="searchParameters"></param>
    /// <returns>The actor entities found by the actor service</returns>
    [HttpGet]
    public override IActionResult List([FromQuery]ActorSearchParameters searchParameters) => Ok(_actorService.ReadList(searchParameters));
}