using System.Linq;
using AutoMapper;
using Dal.DbContext;
using Dal.Interfaces;
using DAL.Abstracts;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Dal.Dals
{
    public class QuestionDal : BasicCrudDalAbstract<Question>, IQuestionDal
    {   
        private readonly IEntityContext _dbContext;

        private readonly IMapper _mapper;

        public QuestionDal(IEntityContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        protected override IMapper GetMapper()
        {
            return _mapper;
        }

        protected override IEntityContext GetDbContext()
        {
            return _dbContext;
        }

        protected override DbSet<Question> GetDbSet()
        {
            return _dbContext.Questions;
        }

        protected override IQueryable<Question> DbSetInclude()
        {
            return base.DbSetInclude()
                .Include(x => x.UserRef)
                .Include(x => x.Tags)
                .Include(x => x.Answers);
        }
    }
}