using SocketChat.Common.Entities;
using SocketChat.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketChat.BLL.Logic
{
    public class MessageLogic : IMessageLogic
    {
        private readonly IMessageRepository _messageRepository;

        public MessageLogic(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task AddAsyng(SignalRMessage message)
        {
            await _messageRepository.AddMessageAsyng(message);
        }

        public Task<List<SignalRMessage>> GetAllAsyng()
        {
            return _messageRepository.GetMessageAsync();
        }
    }
}
