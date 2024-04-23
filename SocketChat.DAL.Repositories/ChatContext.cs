using Microsoft.EntityFrameworkCore;
using SocketChat.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketChat.DAL.Repositories
{
    public class ChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<SignalRMessage> Messages { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {
            Database.EnsureCreated();
            //this.ChangeTracker.LazyLoadingEnabled = true; //
        }

    }
}
