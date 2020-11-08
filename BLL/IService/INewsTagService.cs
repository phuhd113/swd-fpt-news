using BLL.ViewModel.NewsTagModel;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.IService
{
    public interface INewsTagService
    {
        public bool DeleteNewsTag(int tagId);
        public bool CreateNewsTag(NewsTagModel newsTag);

        public IQueryable<Tag> GetTagsByNewsId(int newsId);    
    }
}
