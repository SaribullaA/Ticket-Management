using Microsoft.EntityFrameworkCore;
using Ticket_Management.Entity.Model;

namespace Ticket_Management.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
                
        }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
       
      

    }
}
