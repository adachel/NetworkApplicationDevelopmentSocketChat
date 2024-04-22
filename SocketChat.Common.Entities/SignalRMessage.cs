using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketChat.Common.Entities
{
    [Table("messages")]
    public partial class SignalRMessage
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("text")]
        public string? MessageContent { get; set; }


        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
    }
}
