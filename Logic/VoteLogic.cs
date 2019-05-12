using System;
using System.Threading.Tasks;
using Logic.Interfaces;

namespace Logic
{
    public class VoteLogic : IVoteLogic
    {
        public VoteLogic(IQuestionLogic questionLogic)
        {
            // TODO: hold on to questionLogic as a class property
        }
        
        /// <summary>
        ///     UpVote question with Id = id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task UpVote(Guid id)
        {
            // TODO: UpVote question with Id = id
            throw new NotImplementedException();
        }

        /// <summary>
        ///     DownVote question with Id = id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DownVote(Guid id)
        {
            // TODO: DownVote question with Id = id
            throw new NotImplementedException();
        }
    }
}