using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Worky.Models.Calendar;
using Worky.Data.Project;

namespace Worky.Controllers
{
    [Authorize]
    public class CallendarController : Controller
    {
        IProjectDb col;
        public CallendarController(Data.Project.ProjectDbContext context)
        {
            col = context;
        }
        public IActionResult Month(int pid,int month=-1,int year=-1)
        { 
            Worky.Users.User user = Worky.Users.User.GetUsrByEmail(User.Identity.Name);

            
            Project.Project project = col.GetProject(pid);
            if (project == null)
            {

                return RedirectToAction("NoAccess", "Msg");
            }

            if (user.IsUserAcsessToProhect(pid) == false)
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
