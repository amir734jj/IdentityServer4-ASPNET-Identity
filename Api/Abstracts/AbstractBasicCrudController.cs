using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Abstracts
{
    public abstract class AbstractBasicCrudController<T> : Controller
    {
        [NonAction]
        protected abstract IBasicCrudLogic<T> BasicCrudLogic();

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<>), 200)]
        [SwaggerOperation("GetAll")]
        public virtual async Task<IActionResult> GetAll()
        {
            return Ok(await BasicCrudLogic().GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation("Get")]
        public virtual async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await BasicCrudLogic().Get(id));
        }

        [HttpPut]
        [Route("{id}")]
        [SwaggerOperation("Update")]
        public virtual async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] T instance)
        {
            return Ok(await BasicCrudLogic().Update(id, instance));
        }

        [HttpDelete]
        [Route("{id}")]
        [SwaggerOperation("Delete")]
        public virtual async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(await BasicCrudLogic().Delete(id));
        }
        
        [HttpPost]
        [Route("")]
        [SwaggerOperation("Save")]
        public virtual async Task<IActionResult> Save([FromBody] T instance)
        {
            return Ok(await BasicCrudLogic().Save(instance));
        }
    }
}