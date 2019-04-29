using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Dal.Dals;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Moq;
using Xunit;

namespace Dal.Tests
{
    public class QuestionDalTest
    {
        private readonly Fixture _fixture;
        private readonly IMapper _mapper;

        public QuestionDalTest()
        {
            _fixture = new Fixture();
            _mapper = new MapperConfiguration(x => { }).CreateMapper();
        }
        
        [Fact]
        public async Task Test__GetAll()
        {
            // Arrange
            var questions = _fixture.CreateMany<Question>().ToList().AsQueryable();

            var mockSet = new Mock<DbSet<Question>>();
            mockSet.As<IQueryable<Question>>().Setup(m => m.Provider).Returns(questions.Provider);
            mockSet.As<IQueryable<Question>>().Setup(m => m.Expression).Returns(questions.Expression);
            mockSet.As<IQueryable<Question>>().Setup(m => m.ElementType).Returns(questions.ElementType);
            mockSet.As<IQueryable<Question>>().Setup(m => m.GetEnumerator()).Returns(questions.GetEnumerator());

            var entityDbContextMock = new Mock<EntityDbContext> { CallBase =  true };

            entityDbContextMock.Setup(x => x.Questions).Returns(mockSet.Object);

            var questionDal = new QuestionDal(entityDbContextMock.Object, _mapper);

            // Act
            var result = await questionDal.GetAll();

            // Assert
            Assert.NotEmpty(result);
        }
    }
}