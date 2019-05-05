using AutoFixture;
using AutoMapper;
using Models.Models;
using Models.Profiles;
using Xunit;

namespace Models.Tests
{
    public class QuestionProfileTest
    {
        private readonly Fixture _fixture;
        private readonly IMapper _mapper;

        public QuestionProfileTest()
        {
            _mapper = new MapperConfiguration(opt => opt.AddProfile<QuestionProfile>()).CreateMapper();
            _fixture = new Fixture();
        }
        
        [Fact]
        public void Test__MapToSelf()
        {
            // Arrange
            var question = _fixture
                .Build<Question>()
                .Without(x => x.Answers)
                .Without(x => x.Tags)
                .Create();

            // Act
            var result = _mapper.Map(question, new Question());

            // Assert
            Assert.Equal(question.Text, result.Text);
        }
    }
}