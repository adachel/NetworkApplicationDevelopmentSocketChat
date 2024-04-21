using Microsoft.AspNetCore.Mvc;
using SocketChat.BLL.Logic;
using SocketChat.Common.Entities;

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
        public async Task<List<User>> Get()
        {
            return await  _userLogic.GetAllAsync();
        }


        // POST api/<UserController>
        [HttpPost]
        public async Task Post([FromBody] User user)
        {
            await _userLogic.AddAsync(user);
        }
    }
}
