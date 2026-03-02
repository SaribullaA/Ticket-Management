using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket_Management.Entity;
using Ticket_Management.Repositories;

namespace Ticket_Management.Controllers
{
   // [Authorize] // 🔐 All APIs require JWT by default
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;
        public QuestionController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateQuestion(CreateQuestionRequest createQuestionRequest)
        {
            var result =  await _questionRepository.CreateQuestions(createQuestionRequest);
            return Ok(result);
        }

    }
}
