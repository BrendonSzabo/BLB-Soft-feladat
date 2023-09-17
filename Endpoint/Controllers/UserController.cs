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
    public class UserController : ControllerBase
    {
        /// <summary>
        /// CRUD connection
        /// </summary>
        IUser logic;

        /// <summary>
        /// SignalR connection
        /// </summary>
        IHubContext<SignalRHub> hub;

        public UserController(IUser logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }
        /// <summary>
        /// Returns all Users, Get request
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<User> ReadAll()
        {
            return this.logic.ReadAllUser();
        }

        /// <summary>
        /// Returns a sigle User by id, Get request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public User Read(int id)
        {
            return this.logic.ReadUser(id);
        }

        /// <summary>
        /// Creates new User, Post request
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Create([FromBody] User value)
        {
            this.logic.CreateUser(value);
            this.hub.Clients.All.SendAsync("UserCreated", value);
        }

        /// <summary>
        /// Updates User, Put request
        /// </summary>
        /// <param name="value"></param>
        [HttpPut]
        public void Update([FromBody] User value)
        {
            this.logic.UpdateUser(value);
            this.hub.Clients.All.SendAsync("UserUpdated", value);
        }

        /// <summary>
        /// Deletes User by id, Delete request
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var driverToDelete = this.logic.ReadUser(id);
            this.logic.DeleteUser(id);
            this.hub.Clients.All.SendAsync("UserDeleted", driverToDelete);
        }
    }
}
