using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public sealed class QuestionController : Controller
    {
        private readonly IQuestionLogic _questionLogic;

        public QuestionController(IQuestionLogic questionLogic)
        {
            _questionLogic = questionLogic;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<Question>), 200)]
        [SwaggerOperation("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _questionLogic.GetAll());
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Question), 200)]
        [SwaggerOperation("Get")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await _questionLogic.Get(id));
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(Question), 200)]
        [SwaggerOperation("Update")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] Question instance)
        {
            return Ok(await _questionLogic.Update(id, instance));
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(Question), 200)]
        [SwaggerOperation("Delete")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(await _questionLogic.Delete(id));
        }
        
        [Authorize]
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(Question), 200)]
        [SwaggerOperation("Save")]
        public async Task<IActionResult> Save([FromBody] Question instance)
        {
            return Ok(await _questionLogic.Save(instance));
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("Search/{keyword}")]
        [ProducesResponseType(typeof(List<Question>), 200)]
        [SwaggerOperation("Save")]
        public async Task<IActionResult> Save([FromRoute] string keyword)
        {
            return Ok(await _questionLogic.Search(keyword));
        }
    }
}