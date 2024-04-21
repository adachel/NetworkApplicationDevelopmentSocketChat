using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketChat.Common.Entities
{
    [Table("message")]
    public partial class SignalRMessage
    {
        [Key, Column("id")]
        public int Id { get; set; }
        [Column("text")]
        public string? Message { get; set; }


        //[Column("user_id")]
        

        [ForeignKey("Id")]
        public int UserId { get; set; }
        //public virtual User? User { get; set; }
    }
}
