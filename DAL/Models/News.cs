using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class News
    {
        public News()
        {
            BookMark = new HashSet<BookMark>();
            NewsTag = new HashSet<NewsTag>();
            UserComment = new HashSet<UserComment>();
        }

        public int NewsId { get; set; }
        public string NewsTitle { get; set; }
        public string NewsContent { get; set; }
        public DateTime? DayOfPost { get; set; }
        public int? ChannelId { get; set; }
        public bool? IsActive { get; set; }
        public string LinkImage { get; set; }

        public virtual Channel Channel { get; set; }
        public virtual ICollection<BookMark> BookMark { get; set; }
        public virtual ICollection<NewsTag> NewsTag { get; set; }
        public virtual ICollection<UserComment> UserComment { get; set; }
    }
}
