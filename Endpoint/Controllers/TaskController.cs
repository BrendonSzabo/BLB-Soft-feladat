using Endpoint.Services;
using Logic.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models;

namespace Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        ITask logic;
        IHubContext<SignalRHub> hub;

        public TaskController(ITask logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }
        [HttpGet]
        public IEnumerable<Models.Task> ReadAll()
        {
            return this.logic.ReadAllTask();
        }

        [HttpGet("{id}")]
        public Models.Task Read(int id)
        {
            return this.logic.ReadTask(id);
        }

        [HttpPost]
        public void Create([FromBody] Models.Task value)
        {
            this.logic.CreateTask(value);
            this.hub.Clients.All.SendAsync("TaskCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Models.Task value)
        {
            this.logic.UpdateTask(value);
            this.hub.Clients.All.SendAsync("TaskUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var driverToDelete = this.logic.ReadTask(id);
            this.logic.DeleteTask(id);
            this.hub.Clients.All.SendAsync("TaskDeleted", driverToDelete);
        }
    }
}
