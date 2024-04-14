namespace SocketChat.Common.Entities
{
    public class User
    {
        public string? Name { get; set; }
        public string? Password { get; set; }

        public User(string? name, string? password)
        {
            Name = name;
            Password = password;
        }
    }
}
