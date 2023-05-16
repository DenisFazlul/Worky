using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Worky.Data;
using Worky.Data.Project;
using Worky.Models.Project;
using Worky.Users;

namespace Worky.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        IProjectDb projects;
        IIviteCollection invitesToProject;
        IUsersCollection users;
        public TasksController(ProjectDbContext db, IUsersCollection _users)
        {
            projects = db;
            invitesToProject = db;
            users = _users;
        }
        public IActionResult EditColumnTask(int ProjectId)
        {
             
            List<Worky.Project.Task.TaskStatus> taskStatuses = projects.GetTaskStatuses(ProjectId);
            Models.Project.ShowAllTaskStatusModel model = new Models.Project.ShowAllTaskStatusModel();
            model.ProjectId = ProjectId;
            model.TaskStatuses = taskStatuses;
            return View(model);
        }
        public IActionResult DeleteTaskStatus(int id)
        {
            
            Worky.Project.Task.TaskStatus ts = projects.GetTaskStatusById(id);
            projects.RemoveTaskStatus(ts);

            return RedirectToAction("EditColumnTask", new { ProjectId = ts.ProjectId });
        }
        public IActionResult AddNewTaskStatusId(int ProjectId)
        {
             
            Worky.Project.Task.TaskStatus st = new Project.Task.TaskStatus();
            st.ProjectId = ProjectId;
            st.Name = "New Column";

            projects.AddTaskStatus(st);

            return RedirectToAction("EditColumnTask", new { ProjectId = ProjectId });
        }
        public IActionResult ProjectTasks(int ProjectId)
        {
            
            Worky.Users.User user = users.GetUser(User.Identity.Name);

            Project.Project project = projects.GetProject(ProjectId);
            if(project==null)
            {
               
                return RedirectToAction("NoAccess", "Msg");
            }
            Services.UserAcsessService accesService = new Services.UserAcsessService(invitesToProject, projects);
            
            if (accesService.IsUserAccsessToProject(user, project)== false)
            {
                return RedirectToAction("NoAccess", "Msg");
            }

            TasksSetModel model = new  TasksSetModel();
            model.ProjectId = ProjectId;
            List<Project.Task.Task> tasks = projects.GetTasksForProject(ProjectId);
            List<Worky.Project.Task.TaskStatus> taskStatuses = projects.GetTaskStatuses(project.Id);

            if(taskStatuses.Count==0)
            {
                Worky.Project.Task.TaskStatus.CreateDefultTaskSttusForProject(projects, ProjectId);
                taskStatuses = projects.GetTaskStatuses(project.Id);
            }

            SortTask(tasks, taskStatuses);
            //foreach(Project.Task.TaskStatus taskStatus in taskStatuses)
            //{
            //    taskStatus.Tasks.AddRange(tasks.Where(i => i.TaskStatusId == taskStatus.Id));
            //}
            
                model.Statuses = taskStatuses;
            
            

          


            return View(model);
        }
        private void SortTask(List<Project.Task.Task> tasks, List<Worky.Project.Task.TaskStatus> taskStatuses)
        {
            foreach(Project.Task.Task task in tasks)
            {
                Project.Task.TaskStatus st =  taskStatuses.Where(i => i.Id == task.TaskStatusId).FirstOrDefault();
                if(st==null)
                {
                    taskStatuses[0].Tasks.Add(task);
                }
                else
                {
                    st.Tasks.Add(task);
                }

            }
        }
        public IActionResult AddNewTask(int TaskStatusId)
        {
             
            Worky.Project.Task.TaskStatus st= projects.GetTaskStatusById(TaskStatusId);
            Project.Task.Task task = new Project.Task.Task();
            task.CreationTime = DateTime.Now;
            task.TaskStatusId = st.Id;
            task.ProjectId = st.ProjectId;
            projects.AddTask(task);
            

           
            return RedirectToAction("ProjectTasks", new { ProjectId = st.ProjectId });
        }
        
        [HttpGet]
        public IActionResult EditTaskStatus(int id)
        {
             
            Worky.Project.Task.TaskStatus ts= projects.GetTaskStatusById(id);

            return View(ts);
        }
        [HttpPost]
        public IActionResult EditTaskStatus(Worky.Project.Task.TaskStatus model)
        {
            
            projects.Update(model);

            return RedirectToAction("EditColumnTask", new { ProjectId = model.ProjectId });
        }

    }
}
