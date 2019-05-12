using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal.Interfaces;
using Logic.Interfaces;
using Models.Enums;
using Models.Models;

namespace Logic
{
    public class QuestionLogic : IQuestionLogic
    {
        public QuestionLogic(IQuestionDal questionDal)
        {
            // TODO: hold on to questionDal as a class property
        }

        /// <summary>
        ///     Call forwarding and sort
        /// </summary>
        /// <param name="sortKey"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<Question>> GetAll(SortQuestionsByEnum sortKey)
        {
            // TODO: call the DAL layer and sort result
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Call forwarding
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<Question> Get(Guid id)
        {
            // TODO: call the DAL layer
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Call forwarding
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public virtual async Task<Question> Save(Question instance)
        {
            // TODO: 1) call the DAL layer and 2) set time property to now
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Call forwarding
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<Question> Delete(Guid id)
        {
            // TODO: call the DAL layer
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Call forwarding
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedInstance"></param>
        /// <returns></returns>
        public virtual async Task<Question> Update(Guid id, Question updatedInstance)
        {
            // TODO: 1) call the DAL layer and 2) set time property to now
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Call forwarding
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifyAction"></param>
        /// <returns></returns>
        public virtual async Task<Question> Update(Guid id, Action<Question> modifyAction)
        {
            // TODO: call the DAL layer
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Search the questions
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Question>> Search(string keyword)
        {
            // TODO: Search within the following properties looking for keyword
            //    1) Title
            //    2) Text
            //    3) Answers
            //    4) Tags
            throw new NotImplementedException();
        }
    }
}