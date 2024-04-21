using SocketChat.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketChat.BLL.Logic
{
    public interface IMessageLogic
    {
        Task<List<SignalRMessage>> GetAllAsyng();
        Task AddAsyng(SignalRMessage message);
    }
}
