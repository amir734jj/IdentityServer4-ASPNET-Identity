using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Models.Models;

namespace Models.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, Question>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .EqualityComparison((x, y) => x.Id == y.Id);
        }
    }
}