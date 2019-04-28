using System;
using System.Threading.Tasks;
using API.Abstracts;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class QuestionController : AbstractBasicCrudController<Question>
    {
        private readonly IQuestionLogic _questionLogic;
        private readonly UserManager<User> _userManager;

        public QuestionController(UserManager<User> userManager, IQuestionLogic questionLogic)
        {
            _userManager = userManager;
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
        public override async Task<IActionResult> Save(Question instance)
        {
            instance.UserRef = await _userManager.GetUserAsync(HttpContext.User);

            return await base.Save(instance);
        }

        [Authorize]
        public override async Task<IActionResult> Update(Guid id, Question instance)
        {
            var question = await _questionLogic.Get(id);
            var user = await _userManager.GetUserAsync(HttpContext.User);

            return question.UserRef == user
                ? await base.Update(id, instance)
                : BadRequest("Only question author can update the question!");
        }

        [Authorize]
        public override async Task<IActionResult> Delete(Guid id)
        {
            var question = await _questionLogic.Get(id);
            var user = await _userManager.GetUserAsync(HttpContext.User);

            return question.UserRef == user
                ? await base.Delete(id)
                : BadRequest("Only question author can delete the question!");
        }

        protected override IBasicCrudLogic<Question> BasicCrudLogic()
        {
            return _questionLogic;
        }
    }
}