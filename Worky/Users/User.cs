using System.ComponentModel.DataAnnotations.Schema;
using Worky.Models.Account;

namespace Worky.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string ConfirmCode { get; set; }
        public bool IsConfirmed { get; set; } = false;
        public string UserName { get; set; } = "Новый";
        public bool IsBlock { get; set; } = false;
        [NotMapped]
        public bool IsGuest { get; set; }
       
      

        internal void SetDataFromModel(ChangeUserDataModel model)
        {
            this.UserName = model.UserName;
        }

        internal string GetName()
        {
            if (UserName == "Новый")
            {
                int end = this.Email.IndexOf("@");
                return  this.Email.Substring(0, end);
               
            }
            else
            {
                return UserName;
            }
        }

        public bool IsUserAcsessToProhect(int ProjectId)
        {
            bool val = false;
            Data.Project.IProjectDb Projects = Data.DB.GetProject();
            Project.Project project = Projects.GetProject(ProjectId);
            if(project.UserId==this.Id)
            {
                 val=true;
                this.IsGuest = false;
            }


            Data.IIviteCollection Invites = new Data.Project.ProjectDbContext();
            foreach(Project.Invite inv in Invites.GetInvitesForProject(ProjectId))
            {
                if(inv.UserId==this.Id)
                {
                    return val = true;
                    this.IsGuest = true;
                    break;
                }
            }

            return val;
        }
        public static User GetUsrByEmail(string Email)
        {
            IUsersCollection Users = new Worky.Data.Project.ProjectDbContext();
            Worky.Users.User user = Users.GetUser(Email);
            return user;
        }
        internal void GenerateCode()
        {
            Random r = new Random();
            double val = r.Next(1000, 9999);
            this.ConfirmCode = val.ToString();
        }
        internal void GeneratePass()
        {
            Random r = new Random();
            double val = r.Next(1000, 99999);
           this.Pass= val.ToString();
        }
    }
}
