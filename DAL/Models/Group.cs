using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Group
    {
        public Group()
        {
            User = new HashSet<User>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
