using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Worky.Data.Project;
using Worky.Users;
using Worky.Models.Project;
namespace Worky.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        Data.Project.IProjectDb Projects;
        Users.IUsersCollection Users;
        Data.IIviteCollection Invites;
        public ProjectsController(Data.Project.ProjectDbContext db, IUsersCollection users)
        {
            Projects = db;
            Users = users;
            Invites = db;
        }
        public IActionResult YourProjects()
        {
            int userId = GetUserId();
            if(userId==0)
            {
                return RedirectToAction("Login", "Account");
            }
            Models.Project.UserProjectsModel model = new Models.Project.UserProjectsModel();

            model.UserProject= CreateProjsectModels(Projects.GetProjectsForUser(userId));


            List<Worky.Project.Project> invitesProjects = GetInvitesProject(userId);
            model.InvitesProject = CreateinvitsProjects( invitesProjects,userId);
           

           

            return View(model);
        }
        public List<ProjectModel>CreateinvitsProjects(List<Worky.Project.Project> projects,int UserId)
        {
            List<ProjectModel> models = new List<Models.Project.ProjectModel>();

            foreach (Worky.Project.Project prj in projects)
            {
                InviteningProjectModel model = new InviteningProjectModel();
                model.SetModelDataFromProject(prj);
                model.SetOwner(Users.GetUser(prj.UserId));

                model.invite= Invites.GetInvitedForUser(UserId).Where(i => i.ProjectId == prj.Id).FirstOrDefault();
                models.Add(model);
            }
            return models;
        }

        public List<ProjectModel> CreateProjsectModels(List<Worky.Project.Project> projects)
        {
            List< ProjectModel> models = new List<Models.Project.ProjectModel>();

            foreach (Worky.Project.Project prj in projects)
            {
                Models.Project.ProjectModel model = new Models.Project.ProjectModel();
                model.SetModelDataFromProject(prj);
                model.SetOwner(Users.GetUser(prj.UserId));
              
                models.Add(model);
            }
            return models;
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
