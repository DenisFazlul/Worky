using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Worky.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        public IActionResult EditColumnTask(int ProjectId)
        {
            Data.Project.IProjectDb col = Data.DB.GetProject();
            List<Worky.Project.Task.TaskStatus> taskStatuses = col.GetTaskStatuses(ProjectId);
            Models.Project.ShowAllTaskStatusModel model = new Models.Project.ShowAllTaskStatusModel();
            model.ProjectId = ProjectId;
            model.TaskStatuses = taskStatuses;
            return View(model);
        }
        public IActionResult DeleteTaskStatus(int id)
        {
            Data.Project.IProjectDb col = Data.DB.GetProject();
            Worky.Project.Task.TaskStatus ts = col.GetTaskStatusById(id);
            col.RemoveTaskStatus(ts);

            return RedirectToAction("EditColumnTask", new { ProjectId = ts.ProjectId });
        }
        public IActionResult AddNewTaskStatusId(int ProjectId)
        {
            Data.Project.IProjectDb col = Data.DB.GetProject();
            Worky.Project.Task.TaskStatus st = new Project.Task.TaskStatus();
            st.ProjectId = ProjectId;
            st.Name = "New Column";

            col.AddTaskStatus(st);

            return RedirectToAction("EditColumnTask", new { ProjectId = ProjectId });
        }
        public IActionResult ProjectTasks(int ProjectId)
        {
            Data.Project.IProjectDb col = Data.DB.GetProject();
            Worky.Users.User user = Worky.Users.User.GetUsrByEmail(User.Identity.Name);

            Project.Project project = col.GetProject(ProjectId);
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
            List<Project.Task.Task> tasks = col.GetTasksForProject(ProjectId);
            List<Worky.Project.Task.TaskStatus> taskStatuses = col.GetTaskStatuses(project.Id);

            if(taskStatuses.Count==0)
            {
                project.AddDefultTaskStatus();
                taskStatuses = col.GetTaskStatuses(project.Id);
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
            Data.Project.IProjectDb col = Data.DB.GetProject();
            Worky.Project.Task.TaskStatus st= col.GetTaskStatusById(TaskStatusId);
            
            st.AddNewTask();
            return RedirectToAction("ProjectTasks", new { ProjectId = st.ProjectId });
        }
        
        [HttpGet]
        public IActionResult EditTaskStatus(int id)
        {
            Data.Project.IProjectDb col = Data.DB.GetProject();
            Worky.Project.Task.TaskStatus ts= col.GetTaskStatusById(id);

            return View(ts);
        }
        [HttpPost]
        public IActionResult EditTaskStatus(Worky.Project.Task.TaskStatus model)
        {
            Data.Project.IProjectDb col = Data.DB.GetProject();
            col.Update(model);

            return RedirectToAction("EditColumnTask", new { ProjectId = model.ProjectId });
        }

    }
}
