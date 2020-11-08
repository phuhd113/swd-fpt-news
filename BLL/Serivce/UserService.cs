using BLL.IService;
using BLL.ViewModel.UserModels;
using DAL.Models;
using NewsFPT.DAL.Repositories;
using NewsFPT.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Serivce
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<User> _repo;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = _unitOfWork.GetRepository<User>();
        }
        
        public bool CreateUser(User userModel)
        {
            bool check = false;
            if (userModel != null)
            {
                try
                {
                    if(!_repo.GetAll().Any(x => x.Email.Equals(userModel.Email)))
                    {
                        User user = new User()
                        {

                            Email = userModel.Email,
                            IsAdmin = userModel.IsAdmin,
                            GroupId = userModel.GroupId,
                            Password = userModel.Password,
                            Group = userModel.Group,
                            IsActive = true,
                        };
                        _repo.Add(user);
                        _unitOfWork.Commit();
                        check = true;
                    }                   
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }


            }
            return check;
        }

        public bool LoginUser(UserForLoginModel user)
        {
            var check = _repo.GetAll().Where(x => x.Email == user.Email && x.Password == user.Password && x.IsAdmin == true).Any();
            return check;
        }

        public IQueryable<User> GetAllUsers()
        {
            var users = _repo.GetAll();
            return users;
        }

        public User GetUserById(int id)
        {
            var user = _repo.GetById(id);
            return user;
        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
