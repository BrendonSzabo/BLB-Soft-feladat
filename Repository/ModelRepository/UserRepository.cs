using Models;
using Repository.DbRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ModelRepository
{
    public class UserRepository : Repository<User>, IRepository<User>
    {
        public UserRepository(DatabaseContext ctx) : base(ctx)
        {
        }
        /// <summary>
        /// Returns the first result with the id or null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override User Read(int id)
        {
            return ctx.Users.FirstOrDefault(t => t.Id == id);
        }
        /// <summary>
        /// Updates the old item to the new item by id
        /// </summary>
        /// <param name="item"></param>
        public override void Update(User item)
        {
            var old = Read(item.Id);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
