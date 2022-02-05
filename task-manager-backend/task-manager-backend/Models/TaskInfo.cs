using System.ComponentModel;

namespace TaskManagerBackend.Models
{
    public class TaskInfo
    {
        public string? TaskTitle { get; set; }
        public string? TaskDescription { get; set; }
        [DefaultValue("Low")]
        public string? TaskPriority { get; set; }
        [DefaultValue("2020-2-21")]
        public DateTime? TaskDeadline { get; set; }
        [DefaultValue("Pending")]
        public string? TaskStatus { get; set; }
        public string? Assignee { get; set; }
    }
}
