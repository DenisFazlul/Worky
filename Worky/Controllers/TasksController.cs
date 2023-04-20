using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Worky.Data.Project;

namespace Worky.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        IProjectDb projectDb;
        public TasksController(ProjectDbContext db)
        {
            projectDb = db;
        }
        public IActionResult EditColumnTask(int ProjectId)
        {
             
            List<Worky.Project.Task.TaskStatus> taskStatuses = projectDb.GetTaskStatuses(ProjectId);
            Models.Project.ShowAllTaskStatusModel model = new Models.Project.ShowAllTaskStatusModel();
            model.ProjectId = ProjectId;
            model.TaskStatuses = taskStatuses;
            return View(model);
        }
        public IActionResult DeleteTaskStatus(int id)
        {
            
            Worky.Project.Task.TaskStatus ts = projectDb.GetTaskStatusById(id);
            projectDb.RemoveTaskStatus(ts);

            return RedirectToAction("EditColumnTask", new { ProjectId = ts.ProjectId });
        }
        public IActionResult AddNewTaskStatusId(int ProjectId)
        {
             
            Worky.Project.Task.TaskStatus st = new Project.Task.TaskStatus();
            st.ProjectId = ProjectId;
            st.Name = "New Column";

            projectDb.AddTaskStatus(st);

            return RedirectToAction("EditColumnTask", new { ProjectId = ProjectId });
        }
        public IActionResult ProjectTasks(int ProjectId)
        {
            
            Worky.Users.User user = Worky.Users.User.GetUsrByEmail(User.Identity.Name);

            Project.Project project = projectDb.GetProject(ProjectId);
            if(project==null)
            {
               
                return RedirectToAction("NoAccess", "Msg");
            }
           
            if (user.IsUserAcsessToProhect(ProjectId)==false)
            {
                return RedirectToAction("NoAccess", "Msg");
            }

            Worky.Models.Project.TasksModel model = new Models.Project.TasksModel();
            model.ProjectId = ProjectId;
            List<Project.Task.Task> tasks = projectDb.GetTasksForProject(ProjectId);
            List<Worky.Project.Task.TaskStatus> taskStatuses = projectDb.GetTaskStatuses(project.Id);

            if(taskStatuses.Count==0)
            {
                project.AddDefultTaskStatus();
                taskStatuses = projectDb.GetTaskStatuses(project.Id);
            }

            foreach(Project.Task.TaskStatus taskStatus in taskStatuses)
            {
                taskStatus.Tasks.AddRange(tasks.Where(i => i.TaskStatusId == taskStatus.Id));
            }
            
                model.Statuses = taskStatuses;
            
            

          


            return View(model);
        }
        public IActionResult AddNewTask(int TaskStatusId)
        {
             
            Worky.Project.Task.TaskStatus st= projectDb.GetTaskStatusById(TaskStatusId);
            
            st.AddNewTask();
            return RedirectToAction("ProjectTasks", new { ProjectId = st.ProjectId });
        }
        
        [HttpGet]
        public IActionResult EditTaskStatus(int id)
        {
             
            Worky.Project.Task.TaskStatus ts= projectDb.GetTaskStatusById(id);

            return View(ts);
        }
        [HttpPost]
        public IActionResult EditTaskStatus(Worky.Project.Task.TaskStatus model)
        {
            
            projectDb.Update(model);

            return RedirectToAction("EditColumnTask", new { ProjectId = model.ProjectId });
        }

    }
}
