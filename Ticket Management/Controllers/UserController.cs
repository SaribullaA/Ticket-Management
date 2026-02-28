using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Ticket_Management.Entity;
using Ticket_Management.Entity.Model;
using Ticket_Management.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Ticket_Management.Controllers
{
    [Authorize] // 🔐 All APIs require JWT by default
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
