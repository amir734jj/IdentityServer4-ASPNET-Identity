using System;
using System.Threading.Tasks;
using Models.Models;

namespace Logic.Interfaces
{
    public interface IAnswerLogic
    {
        Task SubmitAnswer(Guid id, Answer answer);
    }
}