namespace Worky.Models.Calendar
{
    public class Day
    {

        public bool IsValid { get; set; } = true;
        public int Number { get; set; }
        public List<ICallendarEvent> Tasks { get; set; }
        public DateTime Date { get;  set; }
        public Day()
        {
            this.Tasks = new List<ICallendarEvent>();
        }

        public void GetDayEvents ()
        {
            GetTask();
        }
        private void GetTask()
        {
            Data.Project.IProjectCollection col = new Data.Project.ProjectDbContext();
            foreach (Worky.Project.Task.Task task in col.GetTaskByDay(Date))
            {
                ICallendarEvent ev = (ICallendarEvent)task;
                this.Tasks.Add(ev);
            }
        }
        
    }
}