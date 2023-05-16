using Worky.Data.Project;
using Worky.Models.Project;

namespace Worky.Services
{
    public class TaskServices
    {
        IProjectDb projects;
        ITaskCommentsDb taskCommentsDb;
        IdFilesDb files;
        public TaskServices(IProjectDb projects,ITaskCommentsDb taskCommentsDb,IdFilesDb files)
        {
            this.projects = projects;
            this.taskCommentsDb = taskCommentsDb;   
            this.files = files;
        }
        public void RemoveTask(int taskId)
        {
            Worky.Project.Task.Task task = projects.GetTaskById(taskId);
            projects.DeleteTask(task);

            List<TaskFile> tfs = projects.GetTaskFiles(taskId);
            foreach(TaskFile tf in tfs)
            {
                projects.RemoveTaskFile(tf);
            }


        }
    }
}
