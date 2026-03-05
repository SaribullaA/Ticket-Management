using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Ticket_Management.Constants;
using Ticket_Management.DBContext;
using Ticket_Management.Entity.Model;
using Ticket_Management.Entity.Request;
using Ticket_Management.Repositories.IRepositories;

namespace Ticket_Management.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext appDbContext;
        public TicketRepository(AppDbContext context)
        {
            appDbContext = context;
        }
        public async Task<string> AddAsync(TicketRequest ticketRequest)
        {
            string status = TicketStatusCodes.MapToStatus(ticketRequest.StatusCode);
            if (status == null)
                return "Invalid status code.";
                    Ticket ticket = new Ticket
                    {
                        Title = ticketRequest.Title,
                        Description = ticketRequest.Description,
                        Status = status,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        AssignTo = ticketRequest.AssignTo,
                    };
            appDbContext.Tickets.Add(ticket);
            await appDbContext.SaveChangesAsync();
            return "Created";
        }

        public async Task DeleteAsync(int id)
        {
            Ticket? ticket = await appDbContext.Tickets.FindAsync(id);
            if (ticket != null)
            {
                appDbContext.Tickets.Remove(ticket);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync(string status = null)
        {
            IQueryable<Ticket> ticket = appDbContext.Tickets.AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                ticket = ticket.Where(t => t.Status.ToLower() == status.ToLower());
            }

            return await ticket.OrderByDescending(t => t.CreatedAt).ToListAsync();
        }

        public async Task<Ticket?> GetByIdAsync(int id)
        {
            return await appDbContext.Tickets.FindAsync(id);
        }

        public async Task<IEnumerable<Ticket>> GetByStatusCodeAsync(int statusCode)
        {
            string status = TicketStatusCodes.MapToStatus(statusCode);
            if (status == null)
                return Enumerable.Empty<Ticket>();

            return await appDbContext.Tickets
                .Where(t => t.Status == status.ToLower())
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsAsync(string? assignTo, int? status)
        {
            IQueryable<Ticket> query = appDbContext.Tickets;
            if (!string.IsNullOrEmpty(assignTo))
            {
                query = query.Where(t => t.AssignTo == assignTo);
            }
            if (status != null)
            {
                string statuss = TicketStatusCodes.MapToStatus((int)status);
                query = query.Where(t => t.Status == statuss);
            }
            return await query.OrderByDescending(t => t.CreatedAt).ToListAsync();
        }

        public async Task<string> UpdateAsync(TicketRequest ticketRequest, string userRole)
        {

            if (ticketRequest == null || ticketRequest.Id <= 0)
                return "Mismatched ticket id.";

            Ticket? existing = await GetByIdAsync(ticketRequest.Id);
            if(existing == null)
            return "Not Found";
            if (ticketRequest.StatusCode != null)
            {
                if (userRole != "Admin")
                    return "Unauthorized: Only admins can change the status.";

                string status = TicketStatusCodes.MapToStatus(ticketRequest.StatusCode);
                if (status == null)
                    return "Invalid status code.";

                existing.Status = status;
            }

            existing.Title = ticketRequest.Title;
            existing.Description = ticketRequest.Description;
            existing.UpdatedAt = DateTime.UtcNow;
            existing.AssignTo = ticketRequest.AssignTo;

            appDbContext.Tickets.Update(existing);
            await appDbContext.SaveChangesAsync();
            return "Updated Successfully";
        }
    }
}
