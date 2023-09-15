using Microsoft.EntityFrameworkCore;
using Models;
using Repository.DbRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ModelRepository
{
    public class TaskRepository : Repository<Models.Task>, IRepository<Models.Task>
    {
        public TaskRepository(DatabaseContext ctx) : base(ctx)
        {
        }

        public override Models.Task Read(int id)
        {
            return ctx.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public override void Update(Models.Task item)
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
