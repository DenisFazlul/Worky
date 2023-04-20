using System.Globalization;

namespace Worky.Models.Calendar
{
    public class Day
    {
        public int ProjectId { get; set; }
        public bool IsValid { get; set; } = true;
        public int Number { get; set; }
        public List<ICallendarEvent> Tasks { get; set; }
        public DateTime Date { get;  set; }
        public string MonthName { get; set; }
        public Day(int Year,int MonthNumber,int DayNumber,int ProjectId)
        {
            this.Tasks = new List<ICallendarEvent>();
            this.Date= new DateTime(Year, MonthNumber, DayNumber);
            this.Number = DayNumber;
            this.MonthName = DateTimeFormatInfo.CurrentInfo.MonthNames[MonthNumber-1];

            StringConverter conv = new StringConverter(this.MonthName);
            this.MonthName = conv.ToUp();
            this.ProjectId = ProjectId;

        }
        public Day()
        {

        }

        public void GetDayEvents ()
        {
            GetTask();
        }
        private void GetTask()
        {
            Data.Project.IProjectDb col = Data.DB.GetProject();
            foreach (Worky.Project.Task.Task task in col.GetTaskByDay(Date,this.ProjectId))
            {
                ICallendarEvent ev = (ICallendarEvent)task;
                this.Tasks.Add(ev);
            }
        }
        
    }
}