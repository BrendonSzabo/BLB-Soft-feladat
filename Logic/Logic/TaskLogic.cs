using Logic.Interface;
using Repository.DbRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Logic
{
    public class TaskLogic : ITask
    {

        IRepository<Models.Task> Repo;
        public TaskLogic(IRepository<Models.Task> repo)
        {
            Repo = repo;
        }
        public void CreateTask(Models.Task item)
        {
            if (item == null || item.Id == null) { throw new Exception("Missing data."); }
            Repo.Create(item);
        }

        public void DeleteTask(int id)
        {
            if (Repo == null) { throw new Exception("No Tasks available."); }
            Repo.Delete(id);
        }

        public IQueryable<Models.Task> ReadAllTask()
        {
            if (Repo == null) { throw new Exception("No Tasks available."); }
            return Repo.ReadAll();
        }

        public Models.Task ReadTask(int id)
        {
            if (Repo == null) { throw new Exception("No Tasks available."); }
            var task = Repo.Read(id);
            if (task == null) { throw new Exception("Task not found."); }
            return task;
        }

        public void UpdateTask(Models.Task item)
        {
            if (Repo == null) { throw new Exception("No Tasks available."); }
            Repo.Update(item);
        }
    }
}
