namespace TaskManagerBackend.Models
{
    public class TaskCreateInfo
    {
        public string? TaskTitle { get; set; }
        public string? TaskDescription { get; set; }
        public string? TaskPriority { get; set; }
        public DateTime? TaskDeadline { get; set; }
        public string Assignee { get; set; }
    }
}
