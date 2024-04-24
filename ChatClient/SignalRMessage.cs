using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TChatClient
{
    public class SignalRMessage
    {
        public int Id { get; set; }
        public string MessageContent { get; set; }
        public int UserID { get; set; }
    }
}
