

using System;
using System.Threading.Tasks;
using Api.Abstracts;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class QuestionController : AbstractBasicCrudController<Question>
    {
        private readonly IQuestionLogic _questionLogic;

        public QuestionController(IQuestionLogic questionLogic)
        {
            _questionLogic = questionLogic;
        }

        [AllowAnonymous]
        public override Task<IActionResult> Get(Guid id)
        {
            return base.Get(id);
        }

        [AllowAnonymous]
        public override Task<IActionResult> GetAll()
        {
            return base.GetAll();
        }

        [Authorize]
        public override async Task<IActionResult> Save([FromBody] Question instance)
        {
            return await base.Save(instance);
        }

        [Authorize]
        public override async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] Question instance)
        {
            return await base.Update(id, instance);
        }

        [Authorize]
        public override async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return await base.Delete(id);
        }

        protected override IBasicCrudLogic<Question> BasicCrudLogic()
        {
            return _questionLogic;
        }
    }
}