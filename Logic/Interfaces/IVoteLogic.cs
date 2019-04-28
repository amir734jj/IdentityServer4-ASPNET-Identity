using System;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IVoteLogic
    {
        Task UpVote(Guid id);
        
        Task DownVote(Guid id);
    }
}