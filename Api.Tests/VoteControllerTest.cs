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
    public class VoteControllerTest
    {
        private readonly Fixture _fixture;

        public VoteControllerTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task UpVote()
        {
            // Arrange
            var question = _fixture
                .Build<Question>()
                .Without(x => x.Answers)
                .Without(x => x.Tags)
                .Create();

            var flag = false;
            
            var voteLogicMock = new Mock<IVoteLogic>();

            voteLogicMock
                .Setup(x => x.UpVote(It.IsAny<Guid>()))
                .Returns(Task.CompletedTask)
                .Callback((Guid _) =>
                {
                    // TODO: set flag to true here
                });

            // TODO: create instance of VoteController using `voteLogicMock` as dependency
            VoteController voteController = null;

            // Act
            await voteController.UpVote(question.Id);

            // Assert
            // TODO: assert flag is true
        }
        
        [Fact]
        public async Task DownVote()
        {
            // Arrange
            var question = _fixture
                .Build<Question>()
                .Without(x => x.Answers)
                .Without(x => x.Tags)
                .Create();

            var flag = false;
            
            var voteLogicMock = new Mock<IVoteLogic>();

            voteLogicMock
                .Setup(x => x.DownVote(It.IsAny<Guid>()))
                .Returns(Task.CompletedTask)
                .Callback((Guid _) => { flag = true; });

            var voteController = new VoteController(voteLogicMock.Object);

            // Act
            await voteController.DownVote(question.Id);

            // Assert
            Assert.True(flag);
        }
    }
}