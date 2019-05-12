using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Dal.Interfaces;
using Models.Enums;
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
            // TODO: create list of Questions using AutoFixture
            List<Question> questions = null;

            // TODO: mock IQuestionDal and setup method GetAll to return `questions`
            Mock<IQuestionDal> questionDalMock = null;
            
            var questionLogic = new QuestionLogic(questionDalMock.Object);

            // Act
            var result = await questionLogic.GetAll(default(SortQuestionsByEnum));

            // Assert
            // TODO: assert questions == result
        }
        
        [Fact]
        public async Task Test__GetAll_Sort()
        {
            // Arrange
            var questions = _fixture
                .Build<Question>()
                .Without(x => x.Tags)
                .With(x => x.Answers, _fixture.Build<Answer>()
                    .Without(y => y.QuestionRef)
                    .CreateMany()
                    .ToList())
                .CreateMany()
                .ToList();
            
            var questionDalMock = new Mock<IQuestionDal>();

            questionDalMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(questions);
            
            var questionLogic = new QuestionLogic(questionDalMock.Object);

            // Act
            var resultSortedByNone = await questionLogic.GetAll(SortQuestionsByEnum.None);
            var resultSortedByVote = await questionLogic.GetAll(SortQuestionsByEnum.Vote);
            var resultSortedByTime = await questionLogic.GetAll(SortQuestionsByEnum.Time);
            var resultSortedByAnswers = await questionLogic.GetAll(SortQuestionsByEnum.Answers);

            // Assert
            Assert.Equal(questions, resultSortedByNone);
            Assert.Equal(questions.OrderByDescending(x => x.Vote), resultSortedByVote);
            Assert.Equal(questions.OrderByDescending(x => x.Time), resultSortedByTime);
            Assert.Equal(questions.OrderByDescending(x => x.Answers.Count), resultSortedByAnswers);
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