using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TaskManagerBackend.Models
{
    public partial class Priority
    {
        public Priority()
        {
            Tasks = new HashSet<Task>();
        }

        public int PriorityID { get; set; }
        public string? PriorityName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
