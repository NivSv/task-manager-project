using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaskManagerBackend.Models
{
    public partial class Priority
    {
        public Priority()
        {
            Tasks = new HashSet<Task>();
        }

        public int PriorityId { get; set; }
        public string? PriorityName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
