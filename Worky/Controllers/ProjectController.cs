using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Worky.Project;
using Worky.Users;

namespace Worky.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        Data.IIviteCollection Invites;
        Users.IUsersCollection Users;
        Data.Project.IProjectDb Projects;
        public ProjectController(Data.Project.ProjectDbContext db, IUsersCollection users)
        {
            Invites = db;
            Users = users;
            Projects = db;

      }
        public IActionResult Index(int ProjectId)
        {
            Project.Project project = Projects.GetProject(ProjectId);
            Worky.Users.User user = Users.GetUser(User.Identity.Name);
            if (project == null)
            {
                return RedirectToAction("NoAccess", "Msg");
               
            }
            Models.Project.ProjectModel model=new Models.Project.ProjectModel(project);
            model.Owner = Users.GetUser(project.UserId);

            model.SetValuesFromProject(project);
            model.SetIvites(Invites.GetInvitesForProject(ProjectId));
            
            
           
            
            if (user.Id == project.UserId)
            {
                model.AllowAddInvites = true;
            }
            Services.UserAcsessService ac = new Services.UserAcsessService(Invites, Projects);
            if(ac.IsUserAccsessToProject(user,project))
            {
                return View(model);
            }
 
            else
            {
                return RedirectToAction("NoAccess", "Msg");
            }


            
        }
        

        private bool IsUserInvittesToProiject(List<Invite> invites)
        {
            int CurUserId = GetUserId();
            if (invites.Where(i => i.UserId == CurUserId).FirstOrDefault() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsUserAcseptedToProject(Project.Project p)
        {
            int CurUserId = GetUserId();
            if (p.UserId == CurUserId)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }

        private int GetUserId()
        {
            string userEmal = User.Identity.Name;
            Worky.Users.User user = Users.GetUser(userEmal);
            return user.Id;
        }
    }
}
