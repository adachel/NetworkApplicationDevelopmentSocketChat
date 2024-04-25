using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SocketChat.Common.Entities;

namespace SocketChat.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatContext _chatContext;
        public SignalRMessage _signalRMessage = new SignalRMessage();

        public UserRepository(ChatContext chatContext)
        {
            _chatContext = chatContext;
        }

        public async Task AddUserAsync(User user)
        {
            var tableData = _chatContext.Users.FirstOrDefault(y => y.Name == user.Name);

            if (tableData != null)
            {
                var newMessage = user.Messages!.ToArray();
                foreach (var item in newMessage)
                {
                    tableData.Messages!.Add(item);
                }
            }
            else
            {
                await _chatContext.Users.AddAsync(user);
            }

            await _chatContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _chatContext.Users.ToListAsync();
        }
    }
}
