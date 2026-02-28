namespace Ticket_Management.Entity
{
    public class TicketRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int StatusCode { get; set; }
        public string AssignTo { get; set; }
    }
}
