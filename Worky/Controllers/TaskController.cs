using Microsoft.AspNetCore.Mvc;

namespace Worky.Controllers
{
    public class TaskController : Controller
    {
        [HttpGet]
        public IActionResult Edit(int TaskId)
        {
            Data.Project.IProjectCollection col = new Data.Project.ProjectDbContext();
            Worky.Project.Task.Task task= col.GetTaskById(TaskId);

            Models.Project.EditTaskModel model = new Models.Project.EditTaskModel(task);
            model.BackLink = $"~/Tasks/ProjectTasks?ProjectId={task.ProjectId}";
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Models.Project.EditTaskModel model)
        {
            Data.Project.IProjectCollection col = new Data.Project.ProjectDbContext();
            Worky.Project.Task.Task task = col.GetTaskById(model.TaskId);
            task.SetData(model);
            col.Update(task);

            return RedirectToAction("Edit", new { TaskId = task.Id });
        }
        public IActionResult AddNewTask(int TaskStatusId)
        {
            Data.Project.IProjectCollection col = new Data.Project.ProjectDbContext();
            Worky.Project.Task.TaskStatus st = col.GetTaskStatusById(TaskStatusId);

            st.AddNewTask();
            return RedirectToAction("ProjectTasks","Tasks", new { ProjectId = st.ProjectId });
        }
        public IActionResult DeleteTask(int TaskId)
        {
            Data.Project.IProjectCollection col = new Data.Project.ProjectDbContext();
            Worky.Project.Task.Task task = col.GetTaskById(TaskId);
            col.DeleteTask(task);
            return RedirectToAction("ProjectTasks", "Tasks", new { ProjectId = task.ProjectId });
        }


    }
}
