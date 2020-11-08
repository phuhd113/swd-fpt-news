using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ViewModel.ChannelsModel
{
    public class ChannelViewModel
    {
        public int ChannelId { get; set; }
        public string ChannelName { get; set; }

        public bool IsActive { get; set; }
    }
}
