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
                // EqualityComparison needs to be supplied when Model is being used in a list
                // and AutoMapper need to map from list to another list
                .EqualityComparison((x, y) => x.Id == y.Id);
        }
    }
}