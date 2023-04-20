namespace Worky.Models.Project
{
    public class ShowAllTaskStatusModel
    {
        public int ProjectId { get; set; }
        public List<Worky.Project.Task.TaskStatus> TaskStatuses { get; set; }
        public ShowAllTaskStatusModel()
        {
            this.TaskStatuses = new List<Worky.Project.Task.TaskStatus>();
        }
    }
}
