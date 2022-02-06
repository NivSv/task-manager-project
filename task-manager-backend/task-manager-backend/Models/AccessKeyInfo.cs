namespace TaskManagerBackend.Models
{
    public class AccessKeyInfo
    {
        public int UserID { get; set; }
        public string? AccessKey { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
