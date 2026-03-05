using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Ticket_Management.Entity.Model;
using Microsoft.AspNetCore.Authorization;
using Ticket_Management.Repositories.IRepositories;
using Ticket_Management.Entity.Request;

namespace Ticket_Management.Controllers
{
    [Authorize] // All APIs require JWT by default
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        public UserController(IUserRepository userRepository)
        {
            _repository = userRepository;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserRequest userRequest)
        {
            string result = await _repository.CreateUser(userRequest);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            string result = await _repository.Login(loginRequest);   
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            IEnumerable<User> users = await _repository.GetAllAsync();
            return Ok(users);
        }
    }
}
