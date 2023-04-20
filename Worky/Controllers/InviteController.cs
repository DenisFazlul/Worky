using Microsoft.AspNetCore.Mvc;
using Worky.Models;
using Worky.Services.EmailService;
using Worky.Users;

namespace Worky.Controllers
{
    public class InviteController : Controller
    {
        Data.IIviteCollection Invites;
        Users.IUsersCollection Users;
        INotificationService notificationService;
        public InviteController(Data.Project.ProjectDbContext invites, Data.Project.ProjectDbContext users,INotificationService s)
        {
            Invites = invites;
            Users = users;
            notificationService = s;
        }
        [HttpGet]
        public IActionResult Add(int ProjectId)
        {
            Models.InviteModel model = new Models.InviteModel();
            model.ProjectId = ProjectId;
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(Models.InviteModel model)
        {
         
            Project.Invite invite = new Project.Invite();
            invite.ProjectId = model.ProjectId;
            invite.UserEmail = model.Email;

            Worky.Users.User user = Users.GetUser(model.Email);
            if (user==null)
            {
                 user = new Worky.Users.User()
                {
                    Email = model.Email,

                };
                user.GeneratePass();

               
                Users.AddUser(user);
                
            }
            invite.UserId = user.Id;
            Invites.AddInvite(invite);

            notificationService.SentInviteToUser(user, model.ProjectId);
         

            return RedirectToAction("Index", "Project", new { ProjectId = model.ProjectId });

            
        }
        public IActionResult Remove(int ProjectId,int InviteId)
        {
            Worky.Project.Invite invite = Invites.GetInvite(InviteId);
            Invites.Delete(invite);
            return RedirectToAction("Index", "Project", new { ProjectId = ProjectId });
        }
    }
}
