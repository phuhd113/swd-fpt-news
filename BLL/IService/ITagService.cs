using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.IService
{
    public interface ITagService
    {
        public bool CreateTag(string tagName);

        public bool DeleteTagByID(int id);
        public IQueryable<Tag> GetAllTag();

    }
}
