using System;
using System.Threading.Tasks;
using AutoFixture;
using Logic.Interfaces;
using Models.Models;
using Moq;
using Xunit;

namespace Logic.Tests
{
    public class AsnwerLogicTest
    {
        private readonly Fixture _fixture;

        public AsnwerLogicTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task SubmitAsnwer()
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

            var questionLogicMock = new Mock<IQuestionLogic>();

            questionLogicMock
                .Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<Action<Question>>()))
                .ReturnsAsync((Guid x, Action<Question> _) => question)
                .Callback((Guid x, Action<Question> action) => { action(question); });
            
            var answerLogic = new AnswerLogic(questionLogicMock.Object);
            
            // Act
            await answerLogic.SubmitAnswer(question.Id, answer);

            // Assert
            // TODO: assert `question.Answers` now contains `answer`
        }
    }
}