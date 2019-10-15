namespace Labb_5
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
