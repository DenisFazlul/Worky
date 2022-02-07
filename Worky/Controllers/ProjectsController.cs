using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Worky.Data.Project;

namespace Worky.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        Data.Project.IProjectCollection Projects;
        Users.IUsersCollection Users;
        Data.IIviteCollection Invites;
        public ProjectsController(Data.Project.ProjectDbContext db, Users.UserDbContext users,Data.InviteDbContext invits)
        {
            Projects = db;
            Users = users;
            Invites = invits;
        }
        public IActionResult YourProjects()
        {
            int userId = GetUserId();
            List<Project.Project> projects = Projects.GetProjectsForUser(userId);
            List<Project.Project> InvitesProject = GetInvitesProject(userId);
            projects.AddRange(InvitesProject);

            return View(projects);
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
            return user.Id;
        }
        public IActionResult Delete(int id)
        {
            Project.Project p = this.Projects.GetProject(id);
            Projects.DeleteProject(p);
            return RedirectToAction("YourProjects");
        }
    }
}
