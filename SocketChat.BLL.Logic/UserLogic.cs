using SocketChat.Common.Entities;
using SocketChat.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketChat.BLL.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _userRepository;

        public UserLogic(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddAsyng(User user)
        {
            await _userRepository.AddUserAsyng(user);
        }

        public async Task<List<User>> GetAllAsyng()
        {
            return await _userRepository.GetUsersAsync();
        }
    }
}
