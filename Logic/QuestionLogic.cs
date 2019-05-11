using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal.Interfaces;
using Logic.Interfaces;
using Models.Models;

namespace Logic
{
    public class QuestionLogic : IQuestionLogic
    {
        private readonly IQuestionDal _questionDal;

        public QuestionLogic(IQuestionDal questionDal)
        {
            _questionDal = questionDal;
        }

        /// <summary>
        ///     Call forwarding
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<Question>> GetAll()
        {
            return await _questionDal.GetAll();
        }

        /// <summary>
        ///     Call forwarding
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<Question> Get(Guid id)
        {
            return await _questionDal.Get(id);
        }

        /// <summary>
        ///     Call forwarding
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public virtual async Task<Question> Save(Question instance)
        {
            return await _questionDal.Save(instance);
        }

        /// <summary>
        ///     Call forwarding
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<Question> Delete(Guid id)
        {
            return await _questionDal.Delete(id);
        }

        /// <summary>
        ///     Call forwarding
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedInstance"></param>
        /// <returns></returns>
        public virtual async Task<Question> Update(Guid id, Question updatedInstance)
        {
            return await _questionDal.Update(id, updatedInstance);
        }

        /// <summary>
        ///     Call forwarding
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifyAction"></param>
        /// <returns></returns>
        public virtual async Task<Question> Update(Guid id, Action<Question> modifyAction)
        {
            return await _questionDal.Update(id, modifyAction);
        }

        /// <summary>
        ///     Search the questions
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Question>> Search(string keyword)
        {
            const StringComparison stringComparisonEnum = StringComparison.OrdinalIgnoreCase;

            return (await GetAll())
                .Where(x => x.Title.Contains(keyword, stringComparisonEnum)
                            || x.Text.Contains(keyword, stringComparisonEnum)
                            || x.Answers.Any(y => y.Text.Contains(keyword, stringComparisonEnum)));
        }
    }
}