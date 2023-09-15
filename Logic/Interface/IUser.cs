using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interface
{
    public interface IUser
    {
        IQueryable<User> ReadAllUser();
        void CreateUser(User item);
        void DeleteUser(int id);
        void UpdateUser(User item);
        User ReadUser(int id);
    }
}
