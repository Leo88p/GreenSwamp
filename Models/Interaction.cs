namespace Lab3.Models
{
    public enum InteractionType
    {
        comment, reribb, lik, rsvp
    }
    public class Interaction
    {
        public long InteractionId { get; set; }
        public long UserId { get; set; }
        public long PostId { get; set; }
        public InteractionType InteractionType { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Content { get; set; }
    }
}
