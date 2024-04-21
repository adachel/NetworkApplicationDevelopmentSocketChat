using SocketChat.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketChat.DAL.Repositories
{
    public interface IMessageRepository
    {
        Task<List<SignalRMessage>> GetMessageAsync();
        Task AddMessageAsyng(SignalRMessage message);
    }
}
