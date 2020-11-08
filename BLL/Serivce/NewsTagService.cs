using BLL.IService;
using BLL.ViewModel.NewsTagModel;
using DAL.Models;
using NewsFPT.DAL.Repositories;
using NewsFPT.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BLL.Serivce
{
    public class NewsTagService : INewsTagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<NewsTag> _newsTag;

        public NewsTagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _newsTag = _unitOfWork.GetRepository<NewsTag>();
        }

        public bool CreateNewsTag(NewsTagModel newsTag)
        {
            bool check = false;
            if (newsTag != null)
            {
                NewsTag newTag = new NewsTag()
                {
                    TagId = newsTag.TagId,
                    NewsId = newsTag.NewsId,
                };
                _newsTag.Add(newTag);
                _unitOfWork.Commit();
                check = true;

            }
            return check;
        }

        public bool DeleteNewsTag(int tagId)
        {
            bool check = false;
            NewsTag newsTag = _newsTag.GetById(tagId);
            if (newsTag != null)
            {

                _newsTag.Delete(newsTag);
                _unitOfWork.Commit();
                check = true;
            }
            return check;
        }  

        public IQueryable<Tag> GetTagsByNewsId(int newsId)
        {
            var tags = _newsTag.GetAll().Where(x => x.NewsId == newsId).Include(x => x.Tag)
                .Select(x => x.Tag);
            return (IQueryable<Tag>)tags;
        }
    }
}
