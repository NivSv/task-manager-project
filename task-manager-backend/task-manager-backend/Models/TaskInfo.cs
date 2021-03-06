using System.ComponentModel;

namespace TaskManagerBackend.Models
{
    public class TaskInfo
    {
        public int TaskId { get; set; }
        public string? TaskTitle { get; set; }
        public string? TaskDescription { get; set; }
        public string? TaskPriority { get; set; }
        public DateTime TaskCreatedDate { get; set; }
        public DateTime TaskDeadline { get; set; }
        public string? TaskStatus { get; set; }
        public string? Assignee { get; set; }
    }
}
