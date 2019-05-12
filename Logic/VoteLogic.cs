using System;
using System.Threading.Tasks;
using Logic.Interfaces;

namespace Logic
{
    public class VoteLogic : IVoteLogic
    {
        private readonly IQuestionLogic _questionLogic;

        public VoteLogic(IQuestionLogic questionLogic)
        {
            _questionLogic = questionLogic;
        }
        
        /// <summary>
        ///     UpVote question with Id = id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task UpVote(Guid id)
        {
            await _questionLogic.Update(id, x =>
            {
                x.Vote++;
            });
        }

        /// <summary>
        ///     DownVote question with Id = id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DownVote(Guid id)
        {
            await _questionLogic.Update(id, x =>
            {
                x.Vote--;
            });
        }
    }
}