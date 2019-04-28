using System;
using System.Threading.Tasks;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AnswerController : Controller
    {
        private readonly IAnswerLogic _answerLogic;

        public AnswerController(IAnswerLogic answerLogic)
        {
            _answerLogic = answerLogic;
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> SubmitAnswer([FromRoute] Guid id, [FromBody] Answer answer)
        {
            await _answerLogic.SubmitAnswer(id, answer);

            return Ok("Updated");
        }
    }
}