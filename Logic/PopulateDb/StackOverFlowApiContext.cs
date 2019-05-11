using System.Threading.Tasks;
using Logic.PopulateDb.Interfaces;
using Logic.PopulateDb.Models;
using RestSharp;

namespace Logic.PopulateDb
{
    public class StackOverFlowApiContext : IStackOverFlowApiContext
    {
        private readonly IRestClient _restClient;

        public StackOverFlowApiContext(IRestClient restClient)
        {
            _restClient = restClient;
        }

        /// <summary>
        ///     Given a tag, it returns StackOverFlow questions
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Task<StackOverFlowResponse> ResolveQuestions(string tag)
        {
            return _restClient.GetAsync<StackOverFlowResponse>(
                new RestRequest("/2.2/questions?pagesize=50&site=stackoverflow&sort=hot&filter=withbody")
                    .AddQueryParameter("tagged", tag)
            );
        }

        /// <summary>
        ///     Given question Id, return all answers
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public Task<StackOverFlowResponse> ResolveAnswers(int questionId)
        {
            return _restClient.GetAsync<StackOverFlowResponse>(new RestRequest(
                $"2.2/questions/{questionId}?order=desc&pagesize=50&site=stackoverflow&filter=withbody"));
        }
    }
}