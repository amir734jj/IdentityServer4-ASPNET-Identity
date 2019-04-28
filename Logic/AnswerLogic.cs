using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.Interfaces;
using Models.Models;

namespace Logic
{
    public class AnswerLogic : IAnswerLogic
    {
        private readonly IQuestionLogic _questionLogic;

        public AnswerLogic(IQuestionLogic questionLogic)
        {
            _questionLogic = questionLogic;
        }
        
        public async Task SubmitAnswer(Guid id, Answer answer)
        {
            await _questionLogic.Update(id, x =>
            {
                (x.Answers ?? new List<Answer>()).Add(answer);
            });
        }
    }
}