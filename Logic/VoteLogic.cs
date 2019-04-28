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
        
        public async Task UpVote(Guid id)
        {
            await _questionLogic.Update(id, x =>
            {
                x.Vote++;
            });
        }

        public async Task DownVote(Guid id)
        {
            await _questionLogic.Update(id, x =>
            {
                x.Vote--;
            });
        }
    }
}