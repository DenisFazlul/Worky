using Worky.Users;

namespace Worky.Models.Account
{
    public class ChangeUserDataModel
    {
        public ChangeUserDataModel(User user)
        {
            this.UserName = user.UserName;
            
        }
        public ChangeUserDataModel()
        {

        }

        public string UserName { get; set; }
       

    }
}
