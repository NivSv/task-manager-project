using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaskManagerBackend.Models
{
    public partial class Status
    {
        public Status()
        {
            Tasks = new HashSet<Task>();
        }

        public int StatusId { get; set; }
        public string? StatusName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
