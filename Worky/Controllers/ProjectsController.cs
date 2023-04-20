using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Worky.Data.Project;

namespace Worky.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        Data.Project.IProjectDb Projects;
        Users.IUsersCollection Users;
        Data.IIviteCollection Invites;
        public ProjectsController(Data.Project.ProjectDbContext db, Data.Project.ProjectDbContext users, Data.Project.ProjectDbContext invits)
        {
            Projects = db;
            Users = users;
            Invites = invits;
        }
        public IActionResult YourProjects()
        {
            int userId = GetUserId();
            if(userId==0)
            {
                return RedirectToAction("Login", "Account");
            }
            Models.Project.UserProjectsModel model = new Models.Project.UserProjectsModel();

            model.UserProject=  model.ConvertProjectsToModels(Projects.GetProjectsForUser(userId));
            model.InvitesProject = model.ConvertProjectsToModels(GetInvitesProject(userId));
           

           

            return View(model);
        }

        private List<Project.Project> GetInvitesProject(int userId)
        {
            List<Project.Project> InvitesProject = new List<Project.Project>();
            foreach (Project.Invite i in Invites.GetInvites(userId))
            {
                Project.Project p = this.Projects.GetProject(i.ProjectId);
                InvitesProject.Add(p);
            }
            return InvitesProject;
        }

        public IActionResult AddProject()
        {
            Project.Project pr = new Project.Project();
            pr.UserId = GetUserId();
            pr.Name = "New project";
            pr.Description = "Project Description";
            Projects.AddProject(pr);

            return RedirectToAction("YourProjects");
        }
        [HttpGet]
        public IActionResult EditProject(int id)
        {
            Project.Project project = Projects.GetProject(id);
            Models.Project.EditProjectModel model = new Models.Project.EditProjectModel(project);
            return View(model);
        }
        [HttpPost]
        public IActionResult EditProject(Models.Project.EditProjectModel model)
        {
            Project.Project project = Projects.GetProject(model.PorjecId);
            model.SetSettingsToPrj(project);
            Projects.Update(project);
            return RedirectToAction("YourProjects");

        }


        private int GetUserId()
        {
          string userEmal=  User.Identity.Name;
          Worky.Users.User user=  Users.GetUser(userEmal);
            if (user == null)
            {
                return 0;
            }
            else
            {
                return user.Id;
            }
        }
        public IActionResult Delete(int id)
        {
            Project.Project p = this.Projects.GetProject(id);
            Projects.DeleteProject(p);
            return RedirectToAction("YourProjects");
        }
        public IActionResult Test()
        {
            Models.Message msg = new Models.Message(
                   "Нет доступа",
                   "У вас нет доступа к этому проекту, обратитесь к владельцу проекта для приглашения",
                    "/Projects/Yourprojects",
                    "К моим проектам"

                   );

            
            return RedirectToAction("Message", "msg", msg);
        }
    }
}
