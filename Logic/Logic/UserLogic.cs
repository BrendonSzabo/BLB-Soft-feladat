using Logic.Interface;
using Models;
using Repository.DbRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Logic
{
    public class UserLogic : IUser
    {
        IRepository<User> Repo;
        public UserLogic(IRepository<User> repo)
        {
                Repo = repo;
        }
        public void CreateUser(User item)
        {
            if (item == null || item.Id == null) { throw new Exception("Missing data."); }
            Repo.Create(item);
        }

        public void DeleteUser(int id)
        {
            if (Repo == null) { throw new Exception("No Users available."); }
            Repo.Delete(id);
        }

        public IQueryable<User> ReadAllUser()
        {
            if (Repo == null) { throw new Exception("No Users available."); }
            return Repo.ReadAll();
        }

        public User ReadUser(int id)
        {
            if (Repo == null) { throw new Exception("No Users available."); }
            var user = Repo.Read(id);
            if (user == null) { throw new Exception("User not found."); }
            return user;
        }

        public void UpdateUser(User item)
        {
            if (Repo == null) { throw new Exception("No Users available."); }
            Repo.Update(item);
        }
    }
}
