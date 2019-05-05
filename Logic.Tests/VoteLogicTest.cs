using System;
using System.Threading.Tasks;
using AutoFixture;
using Logic.Interfaces;
using Models.Models;
using Moq;
using Xunit;

namespace Logic.Tests
{
    public class VoteLogicTest : IDisposable
    {
        private readonly Fixture _fixture;

        public VoteLogicTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Test__UpVote()
        {
            // Arrange
            var question = _fixture
                .Build<Question>()
                .Without(x => x.Answers)
                .Without(x => x.Tags)
                .Create();

            var currentVote = question.Vote;
            
            var questionLogicMock = new Mock<IQuestionLogic>();

            questionLogicMock
                .Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<Action<Question>>()))
                .ReturnsAsync((Guid x, Action<Question> _) => question)
                .Callback((Guid x, Action<Question> action) => { action(question); });

            var voteLogic = new VoteLogic(questionLogicMock.Object);
            
            // Act
            await voteLogic.UpVote(question.Id);

            // Assert
            Assert.Equal(currentVote + 1, question.Vote);
        }
        
        [Fact]
        public async Task Test__DownVote()
        {
            // Arrange
            var question = _fixture
                .Build<Question>()
                .Without(x => x.Answers)
                .Without(x => x.Tags)
                .Create();

            var currentVote = question.Vote;
            
            var questionLogicMock = new Mock<IQuestionLogic>();

            questionLogicMock
                .Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<Action<Question>>()))
                .ReturnsAsync((Guid x, Action<Question> _) => question)
                .Callback((Guid x, Action<Question> action) => { action(question); });

            var voteLogic = new VoteLogic(questionLogicMock.Object);
            
            // Act
            await voteLogic.DownVote(question.Id);

            // Assert
            Assert.Equal(currentVote - 1, question.Vote);
        }

        public void Dispose()
        {
            // Tear-down ...
        }
    }
}