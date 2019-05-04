using System.Threading.Tasks;
using Logic.PopulateDb.Models;

namespace Logic.PopulateDb.Interfaces
{
    public interface IStackOverFlowApiContext
    {
        Task<StackOverFlowResponse> ResolveQuestions(string tag);
        
        Task<StackOverFlowResponse> ResolveAnswers(int questionId);
    }
}