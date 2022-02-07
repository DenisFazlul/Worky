using Worky.Users;

namespace Worky.Models.Account
{
    public class UserProfileModel
    {

        public Users.User User { get; set; }
       public string UserEmail { get; set; }
        public UserProfileModel(User existUser)
        {
            this.User= existUser;
            this.UserEmail = User.Email;
        }

        
    }
}
