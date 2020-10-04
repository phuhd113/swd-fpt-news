using BLL.Models.NewsTagModel;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.IService
{
    public interface INewsTagService
    {
        public IQueryable<NewsTag> GetNewsTag();
        public NewsTag GetModelsById(int id);
        public bool DeleteTagNews(int tagId);
        public bool CreateTagNews(NewsTagModel newsTag);
    }
}
