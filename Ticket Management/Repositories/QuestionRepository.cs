using Microsoft.EntityFrameworkCore;
using Ticket_Management.DBContext;
using Ticket_Management.Entity;
using Ticket_Management.Entity.Model;

namespace Ticket_Management.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext _appDbContext;
        public QuestionRepository(AppDbContext appDbContext)
        {
                _appDbContext = appDbContext;
        }

        public async Task<string> CreateQuestions(CreateQuestionRequest createQuestionRequest)
        {
            Question question = new Question
            {
                Questions = createQuestionRequest.Questions,
                CreatedDate = DateTime.Now,
            };
            _appDbContext.Questions.Add(question);
            await _appDbContext.SaveChangesAsync();


      
            foreach (var option in createQuestionRequest.createQuestionOptionRequests)
            {
                QuestionOption newOption = new QuestionOption
                {
                    Option = option.Option,
                    CreatedDate = DateTime.Now,
                    QuestionId = question.Id

                };
                _appDbContext.QuestionOptions.Add(newOption);
            }
            await _appDbContext.SaveChangesAsync();
            return "Question Created Successfully";
        }
    }
}
