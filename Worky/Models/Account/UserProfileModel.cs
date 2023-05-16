using Worky.Project;
using Worky.Users;

namespace Worky.Models.Account
{
    public class UserProfileModel
    {
        internal List<InviteModel> invites;

        public Users.User User { get; set; }
       public string UserEmail { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; internal set; } = false;
        public string ApiKey { get; set; }

        public UserProfileModel(User existUser)
        {
            this.User= existUser;
            this.UserEmail = User.Email;
            this.UserName = existUser.UserName;
        }

        
    }
}
