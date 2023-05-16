using Microsoft.AspNetCore.Mvc;
using Worky.Models;
using Worky.Models.ProjectTeamModels;
using Worky.Users;

namespace Worky.Controllers
{
    public class ProjectTeamController : Controller
    {
        Data.IIviteCollection Invites;
        Users.IUsersCollection Users;
        Data.Project.IProjectDb Projects;
        public ProjectTeamController(Data.Project.ProjectDbContext db, IUsersCollection users)
        {
            Invites = db;
            Users = users;
            Projects = db;
        }
        public IActionResult Team(int pid)
        {
            ProjectTeamModel model = new ProjectTeamModel();
            User user = Users.GetUser(User.Identity.Name);
            Project.Project project = Projects.GetProject(pid);
            if(project.UserId==user.Id)
            {
                model.AccsessToAAddUserToProject = true;
            }    
            List<InviteModel> invites= InviteModel.CreateInvitesMOdels(Invites.GetInvitesForProject(pid), Users,Projects);
                     
            
            model.ProjectId = pid;
            model.invites = invites;
            return View(model);
        }
        public IActionResult DetailTeammate(int inviteId)
        {
            DetailTeammateModel model=new DetailTeammateModel();
            

            Project.Invite invite= this.Invites.GetInvite(inviteId);
            Worky.Users.User inviteUser = Users.GetUser(invite.UserId);

            model.SetInvite(invite);
            model.SetUser(inviteUser);


            return View(model);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
