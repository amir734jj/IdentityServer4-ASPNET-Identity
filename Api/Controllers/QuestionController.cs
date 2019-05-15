using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Enums;
using Models.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public sealed class QuestionController : Controller
    {
        public QuestionController(IQuestionLogic questionLogic)
        {
            // TODO: hold on to questionLogic as a class property
        }

        /// <summary>
        ///     Returns all questions
        /// </summary>
        /// <param name="sortKey"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<Question>), 200)]
        [SwaggerOperation("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] SortQuestionsByEnum sortKey = default)
        {
            // TODO: call logic layer
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Get question by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Question), 200)]
        [SwaggerOperation("Get")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            // TODO: call logic layer
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Update a question given Id and DTO
        /// </summary>
        /// <param name="id"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(Question), 200)]
        [SwaggerOperation("Update")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] Question instance)
        {
            // TODO: call logic layer
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Delete a question given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(Question), 200)]
        [SwaggerOperation("Delete")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // TODO: call logic layer
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Save a question given DTO
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(Question), 200)]
        [SwaggerOperation("Save")]
        public async Task<IActionResult> Save([FromBody] Question instance)
        {
            // TODO: call logic layer
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Search questions given a keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("Search")]
        [ProducesResponseType(typeof(List<Question>), 200)]
        [SwaggerOperation("Search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            // TODO: call logic layer
            throw new NotImplementedException();
        }
    }
}