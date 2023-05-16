using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Worky.Models;
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
            Models.Project.ProjectModel model=new Models.Project.ProjectModel();
            model.SetModelDataFromProject(project);

            model.SetOwner(Users.GetUser(project.UserId));
             

             
            model.Invites = InviteModel.CreateInvitesMOdels(Invites.GetInvitesForProject(ProjectId), Users,Projects);



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

        private List<InviteModel> CreateInvitesModels(List<Invite> invites)
        {
           List<InviteModel> model = new List<InviteModel>();
            foreach(Invite i in invites)
            {
                InviteModel imodel = new InviteModel();
                imodel.Id=i.Id;
                User user= Users.GetUser(i.UserId);
                imodel.User = user;
                imodel.Email = user.Email;
                imodel.ProjectId=i.ProjectId;
                model.Add(imodel);
            }
            return model;
        }

       
        private int GetUserId()
        {
            string userEmal = User.Identity.Name;
            Worky.Users.User user = Users.GetUser(userEmal);
            return user.Id;
        }
    }
}
