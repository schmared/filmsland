using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace filmsApi.Controllers;

/// <summary>
/// GetController allows other controllers to inherit from it not requiring Authorization to access endpoint actions
/// </summary>
/// <typeparam name="TParams">Entity type required by the child controller classes to override
/// the endpoint action for searching list results</typeparam>
[AllowAnonymous]
[ApiController]
public class GetController<TParams> : ControllerBase
{
    public virtual IActionResult Get(int id) => NotFound();
    public virtual IActionResult List(TParams searchParameters) => NotFound();
}