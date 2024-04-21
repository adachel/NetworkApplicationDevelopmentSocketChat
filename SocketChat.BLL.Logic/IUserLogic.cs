using SocketChat.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketChat.BLL.Logic
{
    public interface IUserLogic
    {
        Task<List<User>> GetAllAsync();
        Task AddAsync(User user);
    }
}
