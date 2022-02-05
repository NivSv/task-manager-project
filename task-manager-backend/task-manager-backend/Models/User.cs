using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oversight_Project.Models
{
    public partial class User
    {
        public User()
        {
            Tasks = new HashSet<Task>();
        }

        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
