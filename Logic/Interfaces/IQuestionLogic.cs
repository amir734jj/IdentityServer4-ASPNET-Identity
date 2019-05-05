using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Models;

namespace Logic.Interfaces
{
    public interface IQuestionLogic : IBasicCrudLogic<Question>
    {
        Task<IEnumerable<Question>> Search(string keyword);
    }
}