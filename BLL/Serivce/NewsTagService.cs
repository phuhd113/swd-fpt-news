using BLL.IService;
using BLL.Models.NewsTagModel;
using DAL.Models;
using NewsFPT.DAL.Repositories;
using NewsFPT.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Serivce
{
    public class NewsTagService : INewsTagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<NewsTag> _repo;

        public NewsTagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = _unitOfWork.GetRepository<NewsTag>();
        }

        public bool CreateTagNews(NewsTagModel newsTag)
        {
            bool check = false;
            if (newsTag != null)
            {
                NewsTag newTag = new NewsTag()
                {
                    TagId = newsTag.TagId,
                    NewsId = newsTag.NewsId,
                };
                _repo.Add(newTag);
                _unitOfWork.Commit();
                check = true;

            }
            return check;
        }

        public bool DeleteTagNews(int tagId)
        {
            bool check = false;
            NewsTag newsTag = _repo.GetById(tagId);
            if (newsTag != null)
            {

                _repo.Delete(newsTag);
                _unitOfWork.Commit();
                check = true;
            }
            return check;
        }

        public NewsTag GetModelsById(int id)
        {
            NewsTag newsTag = _repo.GetById(id);
            return newsTag;
        }

        public IQueryable<NewsTag> GetNewsTag()
        {
            var news = _repo.GetAll();
            return news;
        }
    }
}
