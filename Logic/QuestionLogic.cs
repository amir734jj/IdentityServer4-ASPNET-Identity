using Dal.Interfaces;
using Logic.Abstracts;
using Logic.Interfaces;
using Models.Models;

namespace Logic
{
    public class QuestionLogic : BasicCrudLogicAbstract<Question>, IQuestionLogic
    {
        private readonly IQuestionDal _questionDal;

        public QuestionLogic(IQuestionDal questionDal)
        {
            _questionDal = questionDal;
        }
        
        protected override IBasicCrudDal<Question> GetBasicCrudDal()
        {
            return _questionDal;
        }
    }
}