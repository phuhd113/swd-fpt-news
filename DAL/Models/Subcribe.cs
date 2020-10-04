using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Subcribe
    {
        public int? UserId { get; set; }
        public int? ChannelId { get; set; }

        public virtual Channel Channel { get; set; }
        public virtual User User { get; set; }
    }
}
