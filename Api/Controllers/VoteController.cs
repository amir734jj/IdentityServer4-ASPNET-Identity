using System;
using System.Threading.Tasks;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class VoteController : Controller
    {
        public VoteController(IVoteLogic voteLogic)
        {
            // TODO: hold on to voteLogic as a class property
        }
        
        [HttpPost]
        [Route("UpVote/{id}")]
        [SwaggerOperation("UpVote")]
        public async Task<IActionResult> UpVote([FromRoute] Guid id)
        {
            // TODO: Call the logic layer and return Ok response
            throw new NotImplementedException();
        }
        
        [HttpPost]
        [Route("DownVote/{id}")]
        [SwaggerOperation("DownVote")]
        public async Task<IActionResult> DownVote([FromRoute] Guid id)
        {
            // TODO: Call the logic layer and return Ok response
            throw new NotImplementedException();
        }
    }
}