using BLL.ViewModel.TagModel;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.ViewModel.NewsModels
{
    public class NewsViewModel
    {
        [Display(Name = "ID")]
        public int NewsId { get; set; }
        [Display(Name = "Title")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string NewsTitle { get; set; }
        [Display(Name = "Content")]
        [StringLength(500, MinimumLength = 3)]
        [Required]
        public string NewsContent { get; set; }
        [Display(Name = "Day of Post")]
        [DataType(DataType.Date)]
        public DateTime? DayOfPost { get; set; }
        public int? ChannelId { get; set; }
        public string LinkImage { get; set; }
        public ICollection<NewsTag> NewsTags { get; set; }
        public ICollection<Tag> Tags { get; set; }


    }
}
