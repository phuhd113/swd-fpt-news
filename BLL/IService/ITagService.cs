using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.IService
{
    public interface ITagService
    {
        public bool CreateTag(string tagName);

        public bool DeleteTagByID(int id);

        public bool UpdateTagByID(int id, string tagName);

        public List<Tag> GetAllTag();

        public Tag GetTagByID(int id);

    }
}
