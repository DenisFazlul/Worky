using Microsoft.AspNetCore.Mvc;

namespace Worky.Controllers
{
    public class TasksController : Controller
    {
        public IActionResult EditColumnTask(int ProjectId)
        {
            Data.Project.IProjectCollection col = new Data.Project.ProjectDbContext();
            List<Worky.Project.Task.TaskStatus> taskStatuses = col.GetTaskStatuses(ProjectId);
            Models.Project.ShowAllTaskStatusModel model = new Models.Project.ShowAllTaskStatusModel();
            model.ProjectId = ProjectId;
            model.TaskStatuses = taskStatuses;
            return View(model);
        }
        public IActionResult DeleteTaskStatus(int id)
        {
            Data.Project.IProjectCollection col = new Data.Project.ProjectDbContext();
            Worky.Project.Task.TaskStatus ts = col.GetTaskStatusById(id);
            col.RemoveTaskStatus(ts);

            return RedirectToAction("EditColumnTask", new { ProjectId = ts.ProjectId });
        }
        public IActionResult AddNewTaskStatusId(int ProjectId)
        {
            Data.Project.IProjectCollection col = new Data.Project.ProjectDbContext();
            Worky.Project.Task.TaskStatus st = new Project.Task.TaskStatus();
            st.ProjectId = ProjectId;
            st.Name = "New Column";

            col.AddTaskStatus(st);

            return RedirectToAction("EditColumnTask", new { ProjectId = ProjectId });
        }
        public IActionResult ProjectTasks(int ProjectId)
        {
            Data.Project.IProjectCollection col = new Data.Project.ProjectDbContext();
            Project.Project project = col.GetProject(ProjectId);

            Worky.Models.Project.TasksModel model = new Models.Project.TasksModel();
            model.ProjectId = ProjectId;
            List<Worky.Project.Task.TaskStatus> taskStatuses = col.GetTaskStatuses(project.Id);
            if(taskStatuses.Count==0)
            {
                project.AddDefultTaskStatus();
                taskStatuses = col.GetTaskStatuses(project.Id);
            }
            
                model.Statuses = taskStatuses;
            
            

            //model.SetTaskStatuses()


            return View(model);
        }
        public IActionResult AddNewTask(int TaskStatusId)
        {
            Data.Project.IProjectCollection col = new Data.Project.ProjectDbContext();
            Worky.Project.Task.TaskStatus st= col.GetTaskStatusById(TaskStatusId);
            
            st.AddNewTask();
            return RedirectToAction("ProjectTasks", new { ProjectId = st.ProjectId });
        }
        
        [HttpGet]
        public IActionResult EditTaskStatus(int id)
        {
            Data.Project.IProjectCollection col = new Data.Project.ProjectDbContext();
            Worky.Project.Task.TaskStatus ts= col.GetTaskStatusById(id);

            return View(ts);
        }
        [HttpPost]
        public IActionResult EditTaskStatus(Worky.Project.Task.TaskStatus model)
        {
            Data.Project.IProjectCollection col = new Data.Project.ProjectDbContext();
            col.Update(model);

            return RedirectToAction("EditColumnTask", new { ProjectId = model.ProjectId });
        }

    }
}
