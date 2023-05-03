namespace Worky.Models.Project
{
    public class TasksSetModel
    {
        public int ProjectId { get; set; }
        public List<Worky.Project.Task.TaskStatus> Statuses { get; set; }
    }
}
