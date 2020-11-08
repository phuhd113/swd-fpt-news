using BLL.IService;
using BLL.Models;
using BLL.ViewModel.NewsModels;
using BLL.ViewModel.TagModel;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using NewsFPT.DAL.Repositories;
using NewsFPT.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;

namespace BLL.Serivce
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<News> _news;
        private readonly ITagService _tagService;

        public NewsService(IUnitOfWork unitOfWork, ITagService tagService)
        {
            _unitOfWork = unitOfWork;
            _news = _unitOfWork.GetRepository<News>();
            _tagService = tagService;
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
                    _news.Add(news);
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
            News news = _news.GetById(id);
            if (news != null)
            {
                news.IsActive = false;
                _news.Update(news);
                _unitOfWork.Commit();
                check = true;
            }
            return check;
        }

        public IQueryable<NewsViewModel> GetAllNews()
        {
            var listNews = _news.GetAll().Where(x => x.IsActive == true)
                                        .Include(x => x.NewsTag).ThenInclude(x => x.Tag)
                                        .Select(x => new NewsViewModel
                                        {
                                            NewsTitle = x.NewsTitle,
                                            ChannelId = x.ChannelId,
                                            DayOfPost = x.DayOfPost,
                                            LinkImage = x.LinkImage,
                                            NewsContent = x.NewsContent,
                                            NewsId = x.NewsId,
                                            NewsTags = x.NewsTag,                                           
                                        })
                                        .OrderByDescending(x => x.DayOfPost);
            Console.WriteLine("Get all news");
            if (listNews.Any())
            {
                Console.WriteLine("check listnews");
                foreach (var n in listNews)
                {
                    if (n.NewsTags.Any())
                    {
                        Console.WriteLine("check newstags");
                        Console.WriteLine("Test : " + n.NewsTitle);
                        foreach (var x in n.NewsTags)
                        {
                            var tags = _tagService.GetAllTag().Where(y => y.TagId == x.TagId).ToList();
                            Console.WriteLine("Hello abc" + tags[0].TagName);
                            n.Tags = tags;
                        }
                    }

                }
            }
            
            return listNews;
        }

        public News GetNewsById(int id)
        {
            News news = _unitOfWork.GetRepository<News>().GetById(id);
            return news;
        }

        public List<NewsViewModel> SearchNewsByTitle(string title, PagingModel pagingModel)
        {
            IEnumerable<NewsViewModel> newsModel = _news.GetAll()
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
                _news.Update(news);
                _unitOfWork.Commit();
                check = true;

            }
            return check;
        }


    }
}
