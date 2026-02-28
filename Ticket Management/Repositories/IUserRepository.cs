
using Ticket_Management.Entity;
using Ticket_Management.Entity.Model;

namespace Ticket_Management.Repositories
{
    public interface IUserRepository
    {
        Task<string> CreateUser(UserRequest request);
        Task<string> Login(LoginRequest loginRequest);
        Task<IEnumerable<User>> GetAllAsync();
    }
}
