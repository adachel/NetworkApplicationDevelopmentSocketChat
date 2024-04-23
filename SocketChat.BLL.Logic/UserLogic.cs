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




        public async Task AddAsync(User user)
        {
            await _userRepository.AddUserAsync(user);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userRepository.GetUsersAsync();
        }
    }
}
