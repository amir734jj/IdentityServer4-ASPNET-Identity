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
        private readonly IVoteLogic _voteLogic;

        public VoteController(IVoteLogic voteLogic)
        {
            _voteLogic = voteLogic;
        }
        
        [HttpPost]
        [Route("UpVote/{id}")]
        [SwaggerOperation("UpVote")]
        public async Task<IActionResult> UpVote([FromRoute] Guid id)
        {
            await _voteLogic.UpVote(id);
                
            return Ok($"UpVoted question with id: {id}");
        }
        
        [HttpPost]
        [Route("DownVote/{id}")]
        [SwaggerOperation("DownVote")]
        public async Task<IActionResult> DownVote([FromRoute] Guid id)
        {
            await _voteLogic.DownVote(id);
                
            return Ok($"DownVote question with id: {id}");
        }
    }
}