using System;
using System.Linq;
using System.Threading.Tasks;
using Dal.Extensions;
using Html2Markdown;
using Logic.Interfaces;
using Logic.PopulateDb.Interfaces;
using Models.Models;

namespace Logic.PopulateDb
{
    public class PopulateDbLogic : IPopulateDbLogic
    {
        private readonly IStackOverFlowApiContext _stackOverFlowApiContext;

        private readonly IQuestionLogic _questionLogic;

        public PopulateDbLogic(IStackOverFlowApiContext stackOverFlowApiContext, IQuestionLogic questionLogic)
        {
            _stackOverFlowApiContext = stackOverFlowApiContext;
            _questionLogic = questionLogic;
        }

        /// <summary>
        ///     Populates the database
        /// </summary>
        /// <returns></returns>
        public async Task Populate()
        {
            var converter = new Converter();

            var questions = await _stackOverFlowApiContext.ResolveQuestions("C#");

            var questionAnswers = questions?.Items?.ToDictionary(x => x,
                x => _stackOverFlowApiContext.ResolveAnswers(x.QuestionId).Result.Items);

            // Very important to wait for each task to finish as DbContext is not thread safe
            questionAnswers.ForEach(x => _questionLogic.Save(new Question
            {
                Time = DateTime.Now,
                Title = x.Key.Title,
                Text = converter.Convert(x.Key.Body),
                Tags = x.Key.Tags.Select(y => new Tag
                {
                    Text = y
                }).ToList(),
                Answers = x.Value.Select(y => new Answer
                {
                    Text = converter.Convert(y.Body)
                }).ToList()
            }).Wait());
        }
    }
}