using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Models.Models;

namespace Models.Profiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, Tag>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .EqualityComparison((x, y) => x.Id == y.Id);
        }
    }
}