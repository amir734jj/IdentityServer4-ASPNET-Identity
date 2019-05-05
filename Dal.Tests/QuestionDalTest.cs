using System;
using System.Linq;
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
        private readonly EntityDbContext _dbContext;

        public QuestionDalTest()
        {
            _fixture = new Fixture();
            _mapper = new MapperConfiguration(x => { }).CreateMapper();
            _dbContext = new EntityDbContext(opt => opt.UseInMemoryDatabase(_fixture.Create<string>()));
        }

        [Fact]
        public async Task Test__Save()
        {
            // Arrange
            var question = _fixture
                .Build<Question>()
                .Without(x => x.Answers)
                .Without(x => x.Tags)
                .Create();

            var questionDal = new QuestionDal(_dbContext, _mapper);

            // Act
            var result = await questionDal.Save(question);

            // Assert
            Assert.Single(_dbContext.Questions);
            Assert.Equal(question, result);
        }

        [Fact]
        public async Task Test__Get()
        {
            // Arrange
            var question = _fixture
                .Build<Question>()
                .Without(x => x.Answers)
                .Without(x => x.Tags)
                .Create();

            await _dbContext.Questions
                .AddAsync(question)
                .ContinueWith(async _ => await _dbContext.SaveChangesAsync());

            var questionDal = new QuestionDal(_dbContext, _mapper);

            // Act
            var result = await questionDal.Get(question.Id);

            // Assert
            Assert.Equal(question, result);
        }

        [Fact]
        public async Task Test__GetAll()
        {
            // Arrange
            var question = _fixture
                .Build<Question>()
                .Without(x => x.Answers)
                .Without(x => x.Tags)
                .Create();

            await _dbContext.Questions
                .AddAsync(question)
                .ContinueWith(async _ => await _dbContext.SaveChangesAsync());

            var questionDal = new QuestionDal(_dbContext, _mapper);

            // Act
            var result = await questionDal.GetAll();

            // Assert
            Assert.Single(result);
            Assert.Equal(question, result.First());
        }

        [Fact]
        public async Task Test__Delete()
        {
            // Arrange
            var question = _fixture
                .Build<Question>()
                .Without(x => x.Answers)
                .Without(x => x.Tags)
                .Create();

            await _dbContext.Questions
                .AddAsync(question)
                .ContinueWith(async _ => await _dbContext.SaveChangesAsync());

            var questionDal = new QuestionDal(_dbContext, _mapper);

            // Act
            var result = await questionDal.Delete(question.Id);

            // Assert
            Assert.Empty(_dbContext.Questions);
            Assert.Equal(question, result);
        }

        public void Dispose()
        {
            // Re-initialize DB context
            _dbContext.Dispose();
        }
    }
}