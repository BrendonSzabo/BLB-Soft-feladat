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
        IUser logic;
        IHubContext<SignalRHub> hub;

        public UserController(IUser logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }
        [HttpGet]
        public IEnumerable<User> ReadAll()
        {
            return this.logic.ReadAllUser();
        }

        [HttpGet("{id}")]
        public User Read(int id)
        {
            return this.logic.ReadUser(id);
        }

        [HttpPost]
        public void Create([FromBody] User value)
        {
            this.logic.CreateUser(value);
            this.hub.Clients.All.SendAsync("UserCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] User value)
        {
            this.logic.UpdateUser(value);
            this.hub.Clients.All.SendAsync("UserUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var driverToDelete = this.logic.ReadUser(id);
            this.logic.DeleteUser(id);
            this.hub.Clients.All.SendAsync("UserDeleted", driverToDelete);
        }
    }
}
