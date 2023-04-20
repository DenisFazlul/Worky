using Microsoft.AspNetCore.Mvc;
using Worky.Models.Project;
using Worky.Project;
using Worky.Project.Task;
using Worky.Users;
using Worky.Data.Project;

namespace Worky.Controllers
{
    public class TaskController : Controller
    {
        IProjectDb col;
        public TaskController(ProjectDbContext db)
        {
            col = db;
        }
        [HttpGet]
        public IActionResult Edit(int TaskId)
        {
            
            Worky.Project.Task.Task task= col.GetTaskById(TaskId);

            task.GetComments();
            task.GetFiles();
            Users.User user = Users.User.GetUsrByEmail(User.Identity.Name);

            Models.Project.EditTaskModel model = new Models.Project.EditTaskModel(task,user);

           
            
            model.BackLink = $"~/Tasks/ProjectTasks?ProjectId={task.ProjectId}";
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Models.Project.EditTaskModel model)
        {
           
            Worky.Project.Task.Task task = col.GetTaskById(model.TaskId);
            task.SetData(model);
            col.Update(task);

            return RedirectToAction("Edit", new { TaskId = task.Id });
        }
        public IActionResult AddNewComment(CommentAddModel model)
        {

            CurrentUser user = new CurrentUser(User);

            TaskComment com = new TaskComment();
            com.TaskId = model.TaskId;
            com.Comment = model.Content;
            com.UserId = user.Id;
            com.DateTime = DateTime.Now;
            col.AddCommentToTask(com);

            return RedirectToAction("Edit", new { TaskId = model.TaskId });
        }
        public IActionResult AddNewTask(int TaskStatusId)
        {
           
             
            Worky.Project.Task.TaskStatus st = col.GetTaskStatusById(TaskStatusId);


            Worky.Project.Task.Task t = new Worky.Project.Task.Task();
            t.CreationTime = DateTime.Now;
            t.TaskStatusId = TaskStatusId;
            t.ProjectId = st.ProjectId;
            
            col.AddTask(t);
            return RedirectToAction("ProjectTasks","Tasks", new { ProjectId = st.ProjectId });
        }
        public IActionResult DeleteTask(int TaskId)
        {
             
            Worky.Project.Task.Task task = col.GetTaskById(TaskId);
            task.Delete();
          

            return RedirectToAction("ProjectTasks", "Tasks", new { ProjectId = task.ProjectId });

        }
       
       
        
        


    }
}
