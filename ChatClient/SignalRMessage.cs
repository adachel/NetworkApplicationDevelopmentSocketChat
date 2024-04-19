using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TChatClient.ADD;

namespace TChatClient
{
    public class SignalRMessage
    {
        //public string? Message { get; set; }
        //public string? FromUser { get; set; }
        //public string? ConnectionId { get; set; }


        public string? Message { get; set; }
        public User? FromUser { get; set; }
        public string? ConnectionId { get; set; }
    }
}
