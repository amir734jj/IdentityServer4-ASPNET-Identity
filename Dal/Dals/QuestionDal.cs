using System.Linq;
using AutoMapper;
using Dal.Interfaces;
using DAL.Abstracts;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Dal.Dals
{
    public class QuestionDal : BasicCrudDalAbstract<Question>, IQuestionDal
    {   
        private readonly EntityDbContext _dbContext;

        private readonly IMapper _mapper;

        public QuestionDal(EntityDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        protected override IMapper GetMapper()
        {
            return _mapper;
        }

        protected override DbContext GetDbContext()
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