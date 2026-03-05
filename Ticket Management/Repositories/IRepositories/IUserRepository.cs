using Ticket_Management.Entity.Model;
using Ticket_Management.Entity.Request;

namespace Ticket_Management.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<string> CreateUser(UserRequest request);
        Task<string> Login(LoginRequest loginRequest);
        Task<IEnumerable<User>> GetAllAsync();
    }
}
