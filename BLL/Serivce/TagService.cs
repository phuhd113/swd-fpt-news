using BLL.IService;
using DAL.Models;
using NewsFPT.DAL.Repositories;
using NewsFPT.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Serivce
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<Tag> _repo;

        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = _unitOfWork.GetRepository<Tag>();
        }

        public bool CreateTag(string tagName)
        {
            if (tagName.Length != 0)
            {
                try
                {
                    _repo.Add(new Tag
                    {
                        TagName = tagName,
                        IsActive = true
                    });
                    _unitOfWork.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return false;
        }

        public bool DeleteTagByID(int id)
        {

            var tag = _repo.GetById(id);
            if (tag != null)
            {
                try
                {                   
                    tag.IsActive = false;
                    _repo.Update(tag);
                    _unitOfWork.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return false;
        }

        public List<Tag> GetAllTag()
        {
            try
            {
                return _repo.GetAll().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public Tag GetTagByID(int id)
        {
            try
            {
                var tag = _repo.GetById(id);
                if (tag.IsActive == true)
                {
                    return tag;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;

        }

        public bool UpdateTagByID(int id, string tagName)
        {
            throw new NotImplementedException();
        }
    }
}
