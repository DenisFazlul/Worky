using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Worky.Models;
using Worky.Models.Account;
using Worky.Services.EmailService;
using Worky.Users;

namespace Worky.Controllers
{
    public class AccountController : Controller
    {
        Users.IUsersCollection Users;
        INotificationService notificationService;
        public AccountController(Data.Project.ProjectDbContext db,INotificationService notify, IUsersCollection users)
        {
            Users = users;
            notificationService = notify;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(RegistrationModel model)
        {
            User ExistUser = Users.GetUser(model.Email);
            if (ExistUser == null)
            {
                User user = new Worky.Users.User()
                {
                    Email = model.Email,
                    Pass = model.Pass,
                    

                };
             
                Users.AddUser(user);

                notificationService.SentConvfirmCode(user);
               
                Models.Account.ConfirmCodeModels m = new Models.Account.ConfirmCodeModels();
                m.Email = model.Email;
                    
                return View("ConfrimCode", m);
            }

            return RedirectToAction("Message", "Msg", new
            {
                Header="Имейл занят",
                Content="Данный пользователь существует",
                Url= "/Account/Registration",
                UrlName="Регистрация"
            });
        }
        
        public async Task<IActionResult> Logout()
        {
            
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult AdminPanel()
        {
            Claim c= User.Claims.Where(i => i.Type == "id").FirstOrDefault();
            AdminPageModel model = new AdminPageModel();
            model.users = Users.GetUsers();
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(Models.Account.LoginModel model)
        {
            User User = null;
            if (model.Email==AdminData.AdminEmail)
            {
                User = Users.GetUser(AdminData.AdminEmail);
                if (User==null)
                {
                    User = new Worky.Users.User() { Email = AdminData.AdminEmail, UserName = "admin", Pass = AdminData.AdminPassword, IsConfirmed = true };
                    Users.AddUser(User);
    
                }
                if(model.IsUserAcsepted(User))
                {
                    Authenticate(User);
                    return RedirectToAction("YourProjects", "Projects");
                }
                else
                {
                    return RedirectToAction("Login");
                }
               
            }

             User = Users.GetUser(model.Email);
            



            if(model.IsUserAcsepted(User))
            {
                if(User.IsConfirmed==false)
                {
                    ///Перебрасывваем на вью с подтверждением пароля
                    Models.Account.ConfirmCodeModels m = new Models.Account.ConfirmCodeModels();
                    m.Email = model.Email;
                    notificationService.SentConvfirmCode(User);
                   
                   
                    return View("ConfrimCode",m);
                }

                
                Authenticate(User);
                return RedirectToAction("YourProjects", "Projects");
            }

            return RedirectToAction("Login");
        }
        private  void Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim("id",user.Id.ToString()),
                new Claim("UserName",user.UserName)
                
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            ClaimsPrincipal cl = new ClaimsPrincipal(id);


            //Настройки кук что бы они созранялись после выхода из браузера
            AuthenticationProperties pr = new AuthenticationProperties();
            pr.IsPersistent = true;
            // установка аутентификационных куки
             HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, cl,pr);
        }
        [HttpGet]
        public IActionResult SentPass()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SentPass(Models.Account.LoginModel model)
        {
            User user = Users.GetUser(model.Email);
            if (user != null)
            {
                notificationService.SentPass(user);
            }

            
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult ConfrimCode()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ConfrimCode(Models.Account.ConfirmCodeModels model)
        {
            User user = Users.GetUser(model.Email);
            if(user!=null)
            {
                if(user.ConfirmCode==model.Code)
                {
                    user.IsConfirmed = true;
                    Users.UpdateUser(user);
                    
                    Authenticate(user);
                    return RedirectToAction("YourProjects","Projects");
                }
            }
            model.ErrorMsg = "Не правильный ключ";
            return View(model);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            string userName = User.Identity.Name;
            User ExistUser = Users.GetUser(userName);
            if (ExistUser != null)
            {

                Models.Account.UserProfileModel model = new Models.Account.UserProfileModel(ExistUser);
                if(ExistUser.Email==AdminData.AdminEmail)
                {
                    model.IsAdmin = true;
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Login");
            }

            
        }
        public User GetCurUser()
        {
            string userName = User.Identity.Name;
            User ExistUser = Users.GetUser(userName);
            return ExistUser;
        }
        [HttpGet]
        public IActionResult ChangePass()
        {
            
            
            return View();
        }
        [HttpPost]
        public IActionResult ChangePass(Models.Account.ChangePassModel model)
        {
            User user = GetCurUser();
            if (user.Pass == model.OldPass)
            {
                user.Pass = model.NewPass;
                Users.UpdateUser(user);
               
                Models.Account.UserProfileModel UserModel = new Models.Account.UserProfileModel(user);
                notificationService.SentPassWasChanged(user);
                
                return View("Profile", UserModel);
            }
            else
            {
                return View("ChangePass");
            }
        }
        [HttpGet]
        public IActionResult ChangeUserData()
        {
            User user = GetCurUser();
            Models.Account.ChangeUserDataModel model = new Models.Account.ChangeUserDataModel(user);
            return View(model);
        }
        [HttpPost]
        public IActionResult ChangeUserData(Models.Account.ChangeUserDataModel model)
        {
            User user = GetCurUser();
            user = this.Users.GetUser(user.Id);
            user.SetDataFromModel(model);
           
            this.Users.UpdateUser(user);
            return RedirectToAction("Profile");
        }
    }
}
