using System;
using System.Threading.Tasks;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AnswerController : Controller
    {
        public AnswerController(IAnswerLogic answerLogic)
        {
            // TODO: hold on to answerLogic as a class property
        }

        [HttpPost]
        [Route("{id}")]
        [SwaggerOperation("SubmitAnswer")]
        public async Task<IActionResult> SubmitAnswer([FromRoute] Guid id, [FromBody] Answer answer)
        {
            // TODO: Call the logic layer and return Ok response
            throw new NotImplementedException();
        }
    }
}