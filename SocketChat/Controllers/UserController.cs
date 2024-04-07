using Microsoft.AspNetCore.Mvc;
using SocketChat.BLL.Logic;
using SocketChat.Common.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocketChat.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserLogic _userLogic;

        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await Task.Run(() => { return _userLogic.GetAll(); });
        }

        // POST api/<UserController>
        [HttpPost]
        public async void Post([FromBody] User user)
        {
            await Task.Run(() => { _userLogic.Add(user); });
        }
    }
}
