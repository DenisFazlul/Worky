using System.ComponentModel.DataAnnotations.Schema;

namespace Worky.Project.Task
{
    public class TaskStatus
    {
        public int Id { get; set; }
        
        public int ProjectId { get; set; }
        public int SortIndex { get; set; }
        public string Name { get; set; } = "New";
        public string Description { get; set; } = "TemplDesk";
        [NotMapped]
        public List<Task> Tasks { get; set; }
        public TaskStatus()
        {
            this.Tasks = new List<Task>();
        }

        internal void AddNewTask()
        {
            Data.Project.IProjectDb col = Data.DB.GetProject();
            Task t = new Task();
            t.CreationTime = DateTime.Now;
            t.TaskStatusId = this.Id;
            t.ProjectId = this.ProjectId;
            col.AddTask(t);
        }
        
    }
}
