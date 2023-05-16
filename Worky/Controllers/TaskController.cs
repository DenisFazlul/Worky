using Microsoft.AspNetCore.Mvc;
using Worky.Models.Project;
using Worky.Project;
using Worky.Project.Task;
using Worky.Users;
using Worky.Data.Project;
using Worky.Data.Tags;
using Worky.Project.Tags;
using Worky.Models.TaskTags;
using Worky.Services;

namespace Worky.Controllers
{
    public class TaskController : Controller
    {
        IProjectDb projectDb;
        IUsersCollection usersDb;
        ITagsDb tagsDb;
        ITaskCommentsDb taskCommentsDb;
        IdFilesDb filesDb;
        public TaskController(ProjectDbContext db,IUsersCollection _users, ITagsDb _tags)
        {
            projectDb = db;
            usersDb = _users;
            tagsDb = _tags;
            taskCommentsDb = db;
        }
        [HttpGet]
        public IActionResult Edit(int TaskId)
        {
            
            Worky.Project.Task.Task task= projectDb.GetTaskById(TaskId);

           
            
            Users.User user = usersDb.GetUser(User.Identity.Name);

            EditTaskModel model = CreateEditModel(task);
            
             
            model.BackLink = $"~/Tasks/ProjectTasks?ProjectId={task.ProjectId}";
            return View(model);
        }
        private EditTaskModel CreateEditModel(Worky.Project.Task.Task task)
        {
           
            Models.Project.EditTaskModel model = new Models.Project.EditTaskModel(task);


            model.Owner = usersDb.GetUser(task.AutorId);
            model.TaskStatuses = projectDb.GetTaskStatuses(task.ProjectId);




           
                 List<CommentModel> comments = GetTaskCommentsForTask(task.Id);
                 model.SetCommentsModel(comments);
           

          

            
                task.TaskFiles = projectDb.GetTaskFiles(task.Id);
                  
            

            
                TagType[] ProjectTags = tagsDb.GetTagTypesForProject(task.ProjectId);
                TagTaskInstance[] tagTaskInstances = tagsDb.GetTagTaskInstancesForTask(task.Id);

                foreach (TagTaskInstance t in tagTaskInstances)
                {
                    TagType type = tagsDb.GetTagTypeByid(t.TagTypeId);
                    TagTaskInstanceModel tagmodel = new TagTaskInstanceModel()
                    {
                        Id = t.Id,
                        Name = type.Name,
                        TagTypeId = type.Id
                    };

                    model.AddTag(tagmodel);
                }
            


           


           

            return model;
        }
        private List<CommentModel> GetTaskCommentsForTask(int TaskId)
        {
            List<CommentModel> modesl = new List<CommentModel>();
            List<TaskComment> taskComments = projectDb.GetTaskCommentsByTaskId(TaskId);
            foreach(TaskComment c in taskComments)
            {
                CommentModel model = new CommentModel(c);
                model.User=usersDb.GetUser(c.UserId);
                modesl.Add(model);

            }




            return modesl;
            
        }
        [HttpPost]
        public IActionResult Edit(Models.Project.EditTaskModel model)
        {
           
            Worky.Project.Task.Task task = projectDb.GetTaskById(model.TaskId);
            task.SetData(model);
            projectDb.Update(task);

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
            projectDb.AddCommentToTask(com);

            return RedirectToAction("Edit", new { TaskId = model.TaskId });
        }
        public IActionResult AddNewTask(int TaskStatusId)
        {
           
             
            Worky.Project.Task.TaskStatus st = projectDb.GetTaskStatusById(TaskStatusId);


            Worky.Project.Task.Task t = new Worky.Project.Task.Task();
            t.CreationTime = DateTime.Now;
            t.TaskStatusId = TaskStatusId;
            t.ProjectId = st.ProjectId;
            
            projectDb.AddTask(t);
            return RedirectToAction("ProjectTasks","Tasks", new { ProjectId = st.ProjectId });
        }
        public IActionResult DeleteTask(int TaskId)
        {
            TaskServices taskServices = new TaskServices(projectDb, taskCommentsDb, filesDb);
            taskServices.RemoveTask(TaskId);

            Worky.Project.Task.Task task = projectDb.GetTaskById(TaskId);
            projectDb.DeleteTask(task);
            
          

            return RedirectToAction("ProjectTasks", "Tasks", new { ProjectId = task.ProjectId });

        }
       
       
        
        


    }
}
