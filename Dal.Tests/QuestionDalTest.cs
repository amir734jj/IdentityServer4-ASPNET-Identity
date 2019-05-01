using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Dal.Dals;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Xunit;

namespace Dal.Tests
{
    public class QuestionDalTest : IDisposable
    {
        private readonly Fixture _fixture;
        private readonly IMapper _mapper;
        private EntityDbContext _dbContext;

        public QuestionDalTest()
        {
            _fixture = new Fixture();
            _mapper = new MapperConfiguration(x => { }).CreateMapper();
            _dbContext = new EntityDbContext(opt => opt.UseInMemoryDatabase());
        }

        [Fact]
        public async Task Test__GetAll()
        {
            // Arrange
            var question = _fixture
                .Build<Question>()
                .Without(x => x.Answers)
                .Without(x => x.Tags)
                .Without(x => x.UserRef)
                .Create();
            
            var questionDal = new QuestionDal(_dbContext, _mapper);

            // Act
            await questionDal.Save(question);
            var result = await questionDal.GetAll();

            // Assert
            Assert.NotEmpty(result);
        }

        public void Dispose()
        {
            _dbContext = new EntityDbContext(opt => opt.UseInMemoryDatabase());
        }
    }
}