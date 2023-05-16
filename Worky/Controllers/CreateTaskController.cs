using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Worky.Models.Project;
using Worky.Project;
using Worky.Users;
using Worky.Data.Project;

namespace Worky.Controllers
{
    public class CreateTaskController : Controller
    {
        IProjectDb projectDb;
        IdFilesDb filesDB;
        public CreateTaskController(ProjectDbContext context)
        {
            projectDb = context;
            filesDB = context;
        }
        public IActionResult Create(int ProjectId)
        {
            
            Models.CreateTaskModel model = new Models.CreateTaskModel();
            model.ProjectId = ProjectId;
            model.BackLink = $"/Tasks/ProjectTasks?ProjectId={ProjectId}";
            var s = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            return RedirectToAction("Crr",new {json=s});
        }
        public IActionResult CreateFromCalledar(int ProjectId, string Date)
        {
            Models.CreateTaskModel model = new Models.CreateTaskModel();
            model.ProjectId = ProjectId;
            model.BackLink = $"/Callendar/Month?pid={ProjectId}";
            var s = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            return RedirectToAction("Crr", new { json = s });
        }
        public IActionResult Crr(string json)
        {
            Models.CreateTaskModel model= JsonConvert.DeserializeObject<Models.CreateTaskModel>(json);
            return View(model);

        }
        [HttpPost]
        public IActionResult AddToDb(Models.CreateTaskModel model, List<IFormFile> files)
        {
            Worky.Project.Task.Task task = model.GetTask(projectDb);
            CurrentUser u = new CurrentUser(User);
            task.AutorId = u.Id;
            projectDb.AddTask(task);
            
             
            foreach(IFormFile f in files)
            {
                DFile file = new DFile(f);
                filesDB.AddFile(file);
               
                TaskFile ts = new TaskFile();
                ts.TaskId = task.Id;
                ts.FileId = file.Id;
                ts.Name = file.Name;
                projectDb.AddTaskFile(ts);
            }

            return Redirect(model.BackLink);
        }
    }
}
