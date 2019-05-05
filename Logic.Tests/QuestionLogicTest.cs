using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Dal.Interfaces;
using Models.Models;
using Moq;
using Xunit;

namespace Logic.Tests
{
    public class QuestionLogicTest : IDisposable
    {
        private readonly Fixture _fixture;

        public QuestionLogicTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Test__GetAll()
        {
            // Arrange
            var questions = _fixture
                .Build<Question>()
                .Without(x => x.Answers)
                .Without(x => x.Tags)
                .CreateMany();
            
            var questionDalMock = new Mock<IQuestionDal>();

            questionDalMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(questions);
            
            var questionLogic = new QuestionLogic(questionDalMock.Object);

            // Act
            var result = await questionLogic.GetAll();

            // Assert
            Assert.Equal(questions, result);
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
            
            var questionDalMock = new Mock<IQuestionDal>();

            questionDalMock
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .ReturnsAsync(question);
            
            var questionLogic = new QuestionLogic(questionDalMock.Object);

            // Act
            var result = await questionLogic.Get(question.Id);

            // Assert
            Assert.Equal(question, result);
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
            
            var questionDalMock = new Mock<IQuestionDal>();

            questionDalMock
                .Setup(x => x.Save(It.IsAny<Question>()))
                .ReturnsAsync(question);
            
            var questionLogic = new QuestionLogic(questionDalMock.Object);

            // Act
            var result = await questionLogic.Save(question);

            // Assert
            Assert.Equal(question, result);
        }
        
        [Fact]
        public async Task Test__Update()
        {
            // Arrange
            var question = _fixture
                .Build<Question>()
                .Without(x => x.Answers)
                .Without(x => x.Tags)
                .Create();
            
            var questionDalMock = new Mock<IQuestionDal>();

            questionDalMock
                .Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<Question>()))
                .ReturnsAsync(question);
            
            var questionLogic = new QuestionLogic(questionDalMock.Object);

            // Act
            var result = await questionLogic.Update(question.Id, question);

            // Assert
            Assert.Equal(question, result);
        }
        
        [Fact]
        public async Task Test__UpdateAction()
        {
            // Arrange
            var question = _fixture
                .Build<Question>()
                .Without(x => x.Answers)
                .Without(x => x.Tags)
                .Create();
            
            var questionDalMock = new Mock<IQuestionDal>();

            questionDalMock
                .Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny< Action<Question>>()))
                .ReturnsAsync(question);
            
            var questionLogic = new QuestionLogic(questionDalMock.Object);

            // Act
            var result = await questionLogic.Update(question.Id, _ => { });

            // Assert
            Assert.Equal(question, result);
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
            
            var questionDalMock = new Mock<IQuestionDal>();

            questionDalMock
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(question);
            
            var questionLogic = new QuestionLogic(questionDalMock.Object);

            // Act
            var result = await questionLogic.Delete(question.Id);

            // Assert
            Assert.Equal(question, result);
        }
        
        [Fact]
        public async Task Test__Search()
        {
            // Arrange
            var questions = _fixture
                .Build<Question>()
                .Without(x => x.Answers)
                .Without(x => x.Tags)
                .CreateMany();
            
            var questionDalMock = new Mock<IQuestionDal>();

            questionDalMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(questions);
            
            var questionLogic = new QuestionLogic(questionDalMock.Object);

            // Act
            var result = await questionLogic.Search(questions.First().Text);

            // Assert
            Assert.NotEmpty(result);
        }

        public void Dispose()
        {
            // Tear-down ...
        }
    }
}