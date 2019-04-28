using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Models.Models;

namespace Models.Profiles
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<Answer, Answer>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .EqualityComparison((x, y) => x.Id == y.Id);
        }
    }
}