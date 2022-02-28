using Microsoft.AspNetCore.Mvc;

namespace Worky.Controllers
{
    public class CallendarController : Controller
    {
        public IActionResult Month(int pid,int month=-1,int year=-1)
        {
            Models.Calendar.CallendarModel model = new Models.Calendar.CallendarModel();
            model.PorojectId = pid;
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
            return RedirectToAction("Month", new { pid = model.PorojectId, month = model.CurMonth.Number, year = model.CurYear });
        }
    }
}
