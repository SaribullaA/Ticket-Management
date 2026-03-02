namespace Ticket_Management.Entity.Model
{
    public class QuestionAnswer
    {
        public int Id { get; set; }
        public string? Answer { get; set; }
        public DateTime CreatedDate { get; set; }

        //forgin key
        public int QuestionId { get; set; }
        public Question? Question { get; set; }

       

    }
}
