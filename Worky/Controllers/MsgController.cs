using Microsoft.AspNetCore.Mvc;

namespace Worky.Controllers
{
    public class MsgController : Controller
    {
        public IActionResult Message(Models.Message msg)
        {
           
            return View(msg);
        }
        public IActionResult NoAccess()
        {
            Models.Message msg = new Models.Message(
                "Нет доступа",
                "У вас нет доступа к этому проекту, обратитесь к владельцу проекта для приглашения",
                 "/Projects/Yourprojects",
                 "К моим проектам"

                );
            return RedirectToAction("Message", "Msg", msg);
        }
      
       
    }
}
