using System;
using System.Threading.Tasks;
using Api.Controllers;
using AutoFixture;
using Logic.Interfaces;
using Models.Models;
using Moq;
using Xunit;

namespace Api.Tests
{
    public class AnswerControllerTest
    {
        private readonly Fixture _fixture;

        public AnswerControllerTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task SubmitAnswer()
        {
            // Arrange
            var question = _fixture
                .Build<Question>()
                .Without(x => x.Answers)
                .Without(x => x.Tags)
                .Create();

            var answer = _fixture
                .Build<Answer>()
                .Without(x => x.QuestionRef)
                .Create();
            
            var flag = false;
            
            var answerLogicMock = new Mock<IAnswerLogic>();

            answerLogicMock
                .Setup(x => x.SubmitAnswer(It.IsAny<Guid>(), It.IsAny<Answer>()))
                .Returns(Task.CompletedTask)
                .Callback((Guid id, Answer _) => { flag = true; });

            var answerController = new AnswerController(answerLogicMock.Object);

            // Act
            await answerController.SubmitAnswer(question.Id, answer);

            // Assert
            Assert.True(flag);
        }
    }
}