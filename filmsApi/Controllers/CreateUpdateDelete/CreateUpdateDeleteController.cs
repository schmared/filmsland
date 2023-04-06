using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace filmsApi.Controllers
{
    /// <summary>
    /// CreateUpdateDeleteController allows other controllers to inherit from it requiring Authorization to access endpoint actions
    /// </summary>
    /// <typeparam name="TEntity">Entity type required by the child controller classes to override the endpoint actions</typeparam>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CreateUpdateDeleteController<TEntity> : ControllerBase
    {        
        [HttpPost]
        public virtual IActionResult Create([FromBody]TEntity entity) => NotFound();
        [HttpPut]
        public virtual IActionResult Update([FromBody]TEntity entity) => NotFound();
        [HttpDelete("{id}")]
        public virtual IActionResult Delete(int id) => NotFound();
    }
}