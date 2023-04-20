namespace Worky.Users
{
    public interface IUsersCollection
    {
        
        public User GetUser(int id);
        public User GetUser(string Email);
            

        public List<User> GetUsers();
        public void AddUser(User user);
        public void DeleteUser(User user);
        public void UpdateUser(User user);

    }
}
