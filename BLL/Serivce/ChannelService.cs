using BLL.IService;
using BLL.Models.ChannelsModel;
using DAL.Models;
using NewsFPT.DAL.Repositories;
using NewsFPT.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Serivce
{
    public class ChannelService : IChannelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<Channel> _repo;

        public ChannelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = _unitOfWork.GetRepository<Channel>();
        }

        public bool CreateNewChannel(ChannelCreateModel channelCreateModel)
        {
            bool check = false;
            if (channelCreateModel != null)
            {
                Channel channel = new Channel()
                {
                    ChannelId = channelCreateModel.ChannelId,
                    ChannelName = channelCreateModel.ChannelName,
                    IsActive = channelCreateModel.IsActive,
                };
                _repo.Add(channel);
                _unitOfWork.Commit();
                check = true;

            }
            return check;
        }

        public bool DeleteChannel(int id)
        {
            bool check = false;
            Channel channel = _unitOfWork.GetRepository<Channel>().GetById(id);
            if (channel != null)
            {
                channel.IsActive = false;
                
                _repo.Update(channel);
                _unitOfWork.Commit();
                check = true;
            }
            return check;
        }

        public IQueryable<Channel> GetAllChannels()
        {
            IQueryable<Channel> channel = _repo.GetAll();
            return channel;
        }

        public Channel GetChannelById(int id)
        {
            Channel channel = _repo.GetById(id);
            return channel;
        }

        public bool UpdateChannel(Channel channelUpdate)
        {
            bool check = false;
            if (channelUpdate != null)
            {
                _repo.Update(channelUpdate);
                _unitOfWork.Commit();
                check = true;
            }
            return check;
        }
    }
}
