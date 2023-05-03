using Microsoft.AspNetCore.Mvc;
using Worky.Users;

namespace Worky.Controllers
{
    public class AdminController : Controller
    {
        Users.IUsersCollection Users;
        public AdminController(Data.Project.ProjectDbContext db, IUsersCollection users)
        {
            this.Users = users;
        }
        public IActionResult Index()
        {
            User cur=Users.GetUser(User.Identity.Name);
            if(cur.Email!=AdminData.AdminEmail)
            {
                return StatusCode(403);
            }
           List<Models.Account.DetailUserModel> model = new List<Models.Account.DetailUserModel>();

         foreach(Worky.Users.User u in Users.GetUsers())

            {
                Models.Account.DetailUserModel m = new Models.Account.DetailUserModel(u);
                
                model.Add(m);

            }
            return View(model);
        }
        public IActionResult RemoveUser(int id)
        {
            User user = Users.GetUser(id);
            Users.DeleteUser(user);
            return RedirectToAction("Index");

        }
        public IActionResult BlockUser(int id)
        {
            User user = Users.GetUser(id);
            user.IsBlock = true;
            Users.UpdateUser(user);
            return RedirectToAction("Index");
        }
        public IActionResult UnBlockUser(int id)
        {
            User user = Users.GetUser(id);
            user.IsBlock = false;
            Users.UpdateUser(user);
            return RedirectToAction("Index");
        }
    }
}
