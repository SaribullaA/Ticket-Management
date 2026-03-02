namespace Ticket_Management.Entity.Model
{
    public class Question
    {
        public int Id { get; set; }
        public string? Questions { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<QuestionOption>? questionOptions { get; set; }
        public ICollection<QuestionAnswer>? QuestionAnswers { get; set; }
    }
}
