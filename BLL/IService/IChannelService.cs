using BLL.ViewModel.ChannelsModel;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.IService
{
    public interface IChannelService
    {
        public IQueryable<Channel> GetAllChannels();
        public Channel GetChannelById(int id);
        public bool CreateNewChannel(ChannelCreateModel channelCreateModel);
        public bool UpdateChannel(Channel channelUpdate);
        public bool DeleteChannel(int id);
    }
}
