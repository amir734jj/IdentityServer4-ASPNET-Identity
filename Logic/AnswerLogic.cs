using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.Interfaces;
using Models.Models;

namespace Logic
{
    public class AnswerLogic : IAnswerLogic
    {
        public AnswerLogic(IQuestionLogic questionLogic)
        {
            // TODO: hold on to questionLogic as a class property
        }
        
        /// <summary>
        ///     Add answer to question with Id = id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        public async Task SubmitAnswer(Guid id, Answer answer)
        {
            // TODO: add answer to question with Id = id
            // NOTE: question.Answer may be null so initialize it if it is not already initialized
        }
    }
}