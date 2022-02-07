using Microsoft.AspNetCore.Mvc;

namespace Worky.Controllers
{
    public class InviteController : Controller
    {
        Data.IIviteCollection Invites;
        Users.IUsersCollection Users;
        public InviteController(Data.InviteDbContext invites, Users.UserDbContext users)
        {
            Invites = invites;
            Users = users;
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
            Worky.Users.User existUser = Users.GetUser(model.Email);
            if(existUser!=null)
            {
                Project.Invite invite = new Project.Invite();
                invite.ProjectId = model.ProjectId;
                invite.UserId = existUser.Id;
                
                Invites.AddInvite(invite);
                return RedirectToAction("Index", "Project", new { ProjectId = model.ProjectId });
            }
            return RedirectToAction("Add",new {ProjectId=model.ProjectId});
        }
        public IActionResult Remove(int ProjectId,int InviteId)
        {
            Worky.Project.Invite invite = Invites.GetInvite(InviteId);
            Invites.Delete(invite);
            return RedirectToAction("Index", "Project", new { ProjectId = ProjectId });
        }
    }
}
