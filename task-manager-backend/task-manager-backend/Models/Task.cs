using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TaskManagerBackend.Models
{
    public partial class Task
    {
        public int TaskId { get; set; }
        public string? TaskTitle { get; set; }
        public string? TaskDescription { get; set; }
        public int TaskPriority { get; set; }
        public int? TaskStatus { get; set; }
        public DateTime? TaskCreatedDate { get; set; }
        public DateTime? TaskDeadline { get; set; }
        public int Assignee { get; set; }
        [JsonIgnore]
        public virtual User AssigneeNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual Priority TaskPriorityNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual Status? TaskStatusNavigation { get; set; }
    }
}
