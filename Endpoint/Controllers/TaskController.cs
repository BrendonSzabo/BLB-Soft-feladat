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
        /// <summary>
        /// CRUD connection
        /// </summary>
        ITask logic;

        /// <summary>
        /// SignalR connection
        /// </summary>
        IHubContext<SignalRHub> hub;

        public TaskController(ITask logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        /// <summary>
        /// Returns all Tasks, Get request
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Models.Task> ReadAll()
        {
            return this.logic.ReadAllTask();
        }

        /// <summary>
        /// Returns a sigle Task by id, Get request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Models.Task Read(int id)
        {
            return this.logic.ReadTask(id);
        }

        /// <summary>
        /// Creates new Task, Post request
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Create([FromBody] Models.Task value)
        {
            this.logic.CreateTask(value);
            this.hub.Clients.All.SendAsync("TaskCreated", value);
        }

        /// <summary>
        /// Updates Task, Put request
        /// </summary>
        /// <param name="value"></param>
        [HttpPut]
        public void Update([FromBody] Models.Task value)
        {
            this.logic.UpdateTask(value);
            this.hub.Clients.All.SendAsync("TaskUpdated", value);
        }

        /// <summary>
        /// Deletes Task by id, Delete request
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var driverToDelete = this.logic.ReadTask(id);
            this.logic.DeleteTask(id);
            this.hub.Clients.All.SendAsync("TaskDeleted", driverToDelete);
        }
    }
}
