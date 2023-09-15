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

        public override User Read(int id)
        {
            return ctx.Users.FirstOrDefault(t => t.Id == id);
        }

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
