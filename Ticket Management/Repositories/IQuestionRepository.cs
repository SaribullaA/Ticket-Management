using Ticket_Management.Entity;

namespace Ticket_Management.Repositories
{
    public interface IQuestionRepository
    {
        Task<string> CreateQuestions(CreateQuestionRequest createQuestionRequest);
    }
}
