﻿using BLL.Models;
using BLL.ViewModel.NewsModels;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.IService
{
    public interface INewsService 
    {
        public bool CreateNews(NewsViewModel newsModel);
        public bool DeleteNews(int id);
        public IQueryable<NewsViewModel> GetAllNews();
        public News GetNewsById(int id);
        public bool UpdateNews(News news);
        public List<NewsViewModel> SearchNewsByTitle(String title, PagingModel pagingModel);

    }
}
