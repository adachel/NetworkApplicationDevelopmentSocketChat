using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocketChat.Common.Entities
{
    [Table("users")]
    public partial class User
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("password")]
        public string? Password { get; set; }

        [Column("email")]
        public string? Email { get; set; } 


        public virtual ICollection<SignalRMessage>? Messages { get; set; }
    }
}
