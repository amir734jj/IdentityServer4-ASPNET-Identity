using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Controllers;
using AutoFixture;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Enums;
using Models.Models;
using Moq;
using Xunit;

namespace Api.Tests
{
    public class QuestionControllerTest
    {
        private readonly Fixture _fixture;

        public QuestionControllerTest()
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
                .CreateMany()
                .ToList();
            
            var questionLogicMock = new Mock<IQuestionLogic>();

            questionLogicMock
                .Setup(x => x.GetAll(It.IsAny<SortQuestionsByEnum>()))
                .ReturnsAsync(questions);
            
            var questionController = new QuestionController(questionLogicMock.Object);

            // Act
            var result = await questionController.GetAll(default(SortQuestionsByEnum));

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var contents = Assert.IsAssignableFrom<List<Question>>(okObjectResult.Value);
            Assert.Equal(questions, contents);
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
            
            var questionLogicMock = new Mock<IQuestionLogic>();

            questionLogicMock
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .ReturnsAsync(question);
            
            var questionController = new QuestionController(questionLogicMock.Object);

            // Act
            var result = await questionController.Get(question.Id);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var contents = Assert.IsAssignableFrom<Question>(okObjectResult.Value);
            Assert.Equal(question, contents);
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
            
            var questionLogicMock = new Mock<IQuestionLogic>();

            questionLogicMock
                .Setup(x => x.Save(It.IsAny<Question>()))
                .ReturnsAsync(question);
            
            var questionController = new QuestionController(questionLogicMock.Object);

            // Act
            var result = await questionController.Save(question);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var contents = Assert.IsAssignableFrom<Question>(okObjectResult.Value);
            Assert.Equal(question, contents);
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
            
            var questionLogicMock = new Mock<IQuestionLogic>();

            questionLogicMock
                .Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<Question>()))
                .ReturnsAsync(question);
            
            var questionController = new QuestionController(questionLogicMock.Object);

            // Act
            var result = await questionController.Update(question.Id, question);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var contents = Assert.IsAssignableFrom<Question>(okObjectResult.Value);
            Assert.Equal(question, contents);
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
            
            var questionLogicMock = new Mock<IQuestionLogic>();

            questionLogicMock
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(question);
            
            var questionController = new QuestionController(questionLogicMock.Object);

            // Act
            var result = await questionController.Delete(question.Id);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var contents = Assert.IsAssignableFrom<Question>(okObjectResult.Value);
            Assert.Equal(question, contents);
        }
    }
}