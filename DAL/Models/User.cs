using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class User
    {
        public User()
        {
            BookMark = new HashSet<BookMark>();
            UserComment = new HashSet<UserComment>();
            UserTag = new HashSet<UserTag>();
        }

        public int UserId { get; set; }
        public bool? IsAdmin { get; set; }
        public int? GroupId { get; set; }
        public bool? IsActive { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<BookMark> BookMark { get; set; }
        public virtual ICollection<UserComment> UserComment { get; set; }
        public virtual ICollection<UserTag> UserTag { get; set; }
    }
}
