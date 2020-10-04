using BLL.IService;
using BLL.Models;
using BLL.Models.NewsModels;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using NewsFPT.DAL.Repositories;
using NewsFPT.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace BLL.Serivce
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<News> _repo;

        public NewsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = _unitOfWork.GetRepository<News>();
        }

        public bool CreateNews(NewsViewModel newsModel)
        {
            bool check = false;
            if (newsModel != null)
            {
                try
                {
                    News news = new News()
                    {

                        NewsTitle = newsModel.NewsTitle,
                        NewsContent = newsModel.NewsContent,
                        DayOfPost = newsModel.DayOfPost,
                        ChannelId = newsModel.ChannelId,
                        LinkImage = newsModel.LinkImage,
                        IsActive = true,
                    };
                    _repo.Add(news);
                    _unitOfWork.Commit();
                    check = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }


            }
            return check;
        }

        public bool DeleteNews(int id)
        {
            bool check = false;
            News news = _repo.GetById(id);
            if (news != null)
            {
                news.IsActive = false;
                _repo.Update(news);
                _unitOfWork.Commit();
                check = true;
            }
            return check;
        }

        public IQueryable<News> GetAllNews()
        {
            var listNews = _repo.GetAll().Where(x => x.IsActive == true)
                                        .Include(x => x.NewsTag).ThenInclude(y => y.Tag)
                                        .Include(x => x.Channel).OrderByDescending(x => x.DayOfPost);
            return listNews;
        }

        public News GetNewsById(int id)
        {
            News news = _unitOfWork.GetRepository<News>().GetById(id);
            return news;
        }

        public List<NewsViewModel> SearchNewsByTitle(string title, PagingModel pagingModel)
        {
            IEnumerable<NewsViewModel> newsModel = _repo.GetAll()
                .Where(a => a.NewsTitle.ToUpper().Contains(title.ToUpper()))
                .Select(x => new NewsViewModel
                {
                    NewsId = x.NewsId,
                    NewsTitle = x.NewsTitle,
                    NewsContent = x.NewsContent
                });
            if(pagingModel != null)
            {
                return newsModel.Take(pagingModel.PageSize * pagingModel.PageNumber).Skip(pagingModel.PageSize).ToList();
            }
            return newsModel.ToList();
                
                
                //.Select(a => new NewsViewModel
                //{
                //    NewsId = a.NewsId,
                //    NewsTitle = a.NewsTitle,
                //    NewsContent = a.NewsContent
                //});
            //if (newsModel != null)
            //{
            //    //var paging = new Paging();
            //    //if (title == null)
            //    //{
            //    //    title = "";
            //    //}
            //    //var searchTitle = title.ToLower();
            //    var result = new List<NewsViewModel>();
            //    result = newsModel
            //        .Where(a => a.NewsTitle.ToLower().Contains(title))
            //        .OrderBy(a => a.NewsTitle)
            //        .Skip(paging.SkipItem(pagingModel.PageNumber, pagingModel.PageSize))
            //        .Take(pagingModel.PageSize)
            //        .ToList();
            //    if (result != null)
            //    {
            //        return result;
            //    }

        }

       

        public bool UpdateNews(News news)
        {
            bool check = false;
            if (news != null)
            {
                _repo.Update(news);
                _unitOfWork.Commit();
                check = true;

            }
            return check;
        }
    }
}
