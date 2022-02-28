using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Worky.Project;

namespace Worky.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        Data.IIviteCollection Invites;
        Users.IUsersCollection Users;
        Data.Project.IProjectCollection Projects;
        public ProjectController(Data.InviteDbContext invites, Users.UserDbContext users, Data.Project.ProjectDbContext projects)
        {
            Invites = invites;
            Users = users;
            Projects = projects;

      }
        public IActionResult Index(int ProjectId)
        {
            Project.Project project = Projects.GetProject(ProjectId);
            
            if(project==null)
            {
                return Content("Проекта не существует");
            }
            Models.Project.ProjectModel model=new Models.Project.ProjectModel();
            
            model.SetValuesFromProject(project);
            model.Invites = Invites.GetInvitesForProject(ProjectId);
            foreach(Worky.Project.Invite i in model.Invites)
            {
                i.UserEmail = Users.GetUser(i.UserId).Email;
                i.UserName = Users.GetUser(i.UserId).UserName;

            }

            if (IsUserAcseptedToProject(project) || IsUserInvittesToProiject(model.Invites))
            {
                return View(model);
            }
            

            return Content("Нет доступа");
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
