using System.ComponentModel.DataAnnotations.Schema;
using Worky.Models.Calendar;
using Worky.Models.Project;

namespace Worky.Project.Task
{
    public class Task
    {
        public int Id { get; set; }
        public int TaskStatusId { get; set; }
        public int ProjectId { get; set; }

        public int AutorId { get; set; }
        public List<TaskComment> TaskComments { get; set; }
        public List<TaskFile> TaskFiles { get; set; }
        public string Name { get; set; } = "New Task";
        public string Description { get; set; } = "TaskDescription";
        public DateTime End { get; set; }
        public DateTime Start { get; set; }
        public DateTime CreationTime { get; set; }
        public string GetShortDescription()
        {
            string val = "";
            double leng = this.Description.Length;
            if(leng<100)
            {
                val = this.Description;
            }
            else
            {
                val = Description.Substring(0, 100);
                val = val + " ...";
            }
           
            return val;
        }

         

        

         

       

        
       

        internal void SetData(EditTaskModel model)
        {
            this.Name = model.Name;
            this.Description = model.Description;
            this.End = model.End;
            this.Start = model.Start;
            this.TaskStatusId = model.CurTaskStatusId;

        }
        public void SetData(Task task)
        {
            this.Name = task.Name;
            this.Description = task.Description;
            this.End = task.End;
            this.Start = task.Start;
            this.TaskStatusId = task.TaskStatusId;
        }
        public TimeData GetTimeData()
        {
            return new TimeData()
            {
                End = this.End,
                Start = this.Start,
                CreationTime = this.CreationTime
            };
        }

        

       

        
    }
}
