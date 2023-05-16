using Worky.Data.Project;
using Worky.Project;
using Worky.Users;

namespace Worky.Models
{
    public class InviteModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectOwnerName { get; set; }
        public Users.User User { get; set; }
        public bool InviteAcsepted { get; private set; }
        public bool UserConfirmed { get; set; } = false;
        public static List<InviteModel> CreateInvitesMOdels(List<Invite> invites, IUsersCollection Users,IProjectDb projects)
        {
            List<InviteModel> models = new List<InviteModel>();
            foreach (Invite i in invites)
            {
                InviteModel model = new InviteModel();
                model.Id = i.Id;
                User user = Users.GetUser(i.UserId);
                Worky.Project.Project project=projects.GetProject(i.ProjectId);
                model.ProjectName= project.Name;
                model.ProjectOwnerName = Users.GetUser(project.UserId).UserName;
                model.InviteAcsepted = i.InviteAcsepted;
                model.UserConfirmed = user.IsConfirmed;
                model.User = user;
                model.Email = user.Email;
                model.ProjectId = i.ProjectId;
                models.Add(model);
            }
            return models;
        }
    }
}
