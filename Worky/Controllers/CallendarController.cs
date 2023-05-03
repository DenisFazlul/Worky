using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Worky.Models.Calendar;
using Worky.Data.Project;
using Worky.Data;
using Worky.Users;

namespace Worky.Controllers
{
    [Authorize]
    public class CallendarController : Controller
    {
        IProjectDb projects;
        IIviteCollection Invites;
        IUsersCollection Users;
        
        public CallendarController(Data.Project.ProjectDbContext context, IUsersCollection users)
        {
            projects = context;
            Invites = context;
            Users = users;
        }
        public IActionResult Month(int pid,int month=-1,int year=-1)
        { 
            Worky.Users.User user = Users.GetUser(User.Identity.Name);

            
            Project.Project project = projects.GetProject(pid);
            if (project == null)
            {

                return RedirectToAction("NoAccess", "Msg");
            }


            Services.UserAcsessService ac = new Services.UserAcsessService(Invites, projects);
            
            if (ac.IsUserAccsessToProject(user, project) == false)
            {
                return RedirectToAction("NoAccess", "Msg");
            }

            Models.Calendar.CallendarModel model = new Models.Calendar.CallendarModel();
            model.ProjectId = pid;
            if (month==-1)
            {
                model.GetCurMonth();
                model.GetCurYear();
            }
            else
            {
                model.SetMonth(month);
                model.SetYear(year);
            }


            model.GetDays();
            
            return View(model);
        }
        [HttpPost]
        public IActionResult Month(Models.Calendar.CallendarModel model)
        {
            model.GetMonthDayes();
            return RedirectToAction("Month", new { pid = model.ProjectId, month = model.CurMonth.Number+1, year = model.CurYear });
        }
        public IActionResult DetailDay(int pid, int month = -1, int year = -1,int DayNumber=-1)
        {
            DayModel model = new DayModel();
            model.Day = new Day(year, month, DayNumber, pid);
            model.Day.GetDayEvents();
            return View(model);
        }
    }
}
