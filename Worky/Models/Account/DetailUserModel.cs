using Worky.Users;

namespace Worky.Models.Account
{
    public class DetailUserModel
    {
        public User user { get; set; }
        public DetailUserModel(User u)
        {
            this.user = u;
            
        }

      
    }
}
