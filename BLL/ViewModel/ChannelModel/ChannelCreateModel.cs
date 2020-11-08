using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.ViewModel.ChannelsModel
{
    public class ChannelCreateModel
    {
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string ChannelName { get; set; }
        public bool IsActive { get; set; }
    }
}
