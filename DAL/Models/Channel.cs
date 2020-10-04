using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Channel
    {
        public Channel()
        {
            News = new HashSet<News>();
        }

        public int ChannelId { get; set; }
        public string ChannelName { get; set; }
        public bool? IsActive { get; set; }
        public int? GroupId { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}
