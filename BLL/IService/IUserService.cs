using BLL.ViewModel.UserModels;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.IService
{
    public interface IUserService
    {
        
        public User GetUserById(int id);
        public bool UpdateUser(User user);

        public bool LoginUser(UserForLoginModel user);

        public bool CreateUser(User user);
        public IQueryable<User> GetAllUsers();
        
    }
}
