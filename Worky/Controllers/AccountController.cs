using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Worky.Models;
using Worky.Users;

namespace Worky.Controllers
{
    public class AccountController : Controller
    {
        Users.IUsersCollection Users;
        public AccountController(Users.UserDbContext db)
        {
            Users = db;
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
                    ConfirmCode = model.GenerateCode()

                };
                Models.EmailService.Email.SentConvfirmCode(user);
                Users.AddUser(user);
           
                Models.Account.ConfirmCodeModels m = new Models.Account.ConfirmCodeModels();
                m.Email = model.Email;
                    
                return View("ConfrimCode", m);
            }
            return Content("Данный пользователь существует");
        }
        
        public async Task<IActionResult> Logout()
        {
            
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Models.Account.LoginModel model)
        {
            User ExistUser = Users.GetUser(model.Email);
            
            if(model.IsUserAcsepted(ExistUser))
            {
                if(!ExistUser.IsConfirmed)
                {
                    Models.Account.ConfirmCodeModels m = new Models.Account.ConfirmCodeModels();
                    m.Email = model.Email;
                   
                    return View("ConfrimCode",m);
                }
                Authenticate(ExistUser);
                return RedirectToAction("YourProjects", "Projects");
            }

            return RedirectToAction("Login");
        }
        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
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
            Models.EmailService.Email.SentPass(user);
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
                    return RedirectToAction("Index","Home");
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
                return View("Profile", UserModel);
            }
            else
            {
                return View("ChangePass");
            }
        }
    }
}
