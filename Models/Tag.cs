namespace Lab3.Models
{
    public class Tag
    {
        public long TagId { get; set; }
        public string TagName { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public long? UsageCount { get; set; }
    }
}
