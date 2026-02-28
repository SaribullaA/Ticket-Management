using Ticket_Management.Entity;
using Ticket_Management.Entity.Model;

namespace Ticket_Management.Repositories
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetAllAsync(string status = null);
        Task<Ticket?> GetByIdAsync(int id);
        Task<string> AddAsync(TicketRequest ticketRequest);
        Task <string>UpdateAsync(TicketRequest ticketRequest);
        Task DeleteAsync(int id);
        Task<IEnumerable<Ticket>> GetByStatusCodeAsync(int statusCode);
        Task<IEnumerable<Ticket>> GetTicketsAsync(string? assignTo, int? status);
    }
}
