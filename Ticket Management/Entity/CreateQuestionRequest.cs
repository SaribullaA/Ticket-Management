namespace Ticket_Management.Entity
{
    public class CreateQuestionRequest
    {
        public string? Questions { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<CreateQuestionOptionRequest> createQuestionOptionRequests { get; set; }
    }
}
