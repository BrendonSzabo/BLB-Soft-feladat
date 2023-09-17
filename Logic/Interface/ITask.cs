using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interface
{
    public interface ITask
    {
        IQueryable<Models.Task> ReadAllTask();
        void CreateTask(Models.Task item);
        void DeleteTask(int id);
        void UpdateTask(Models.Task item);
        Models.Task ReadTask(int id);

    }
}
