namespace Ticket_Management.Entity.Model
{
    public class QuestionOption
    {
        public int Id { get; set; }
        public string? Option { get; set; }
        public DateTime CreatedDate { get; set; }
        //forgin key

        public int QuestionId { get; set; }
        public Question? Question { get; set; }
    }
}
