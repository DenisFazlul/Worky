using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Worky.Models.Project
{
    public class EditTaskModel
    {
        public string BackLink { get; set; }
        public int ProjectId { get; set; }
        public int TaskId { get; set; }


        [DisplayName("Наименование")]
        public string Name { get; set; }


        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Дата начала")]
        public DateTime Start { get; set; }
        [DisplayName("Дата кончания")]
        public DateTime End { get; set; }
        public DateTime CreationTime { get; set; }
        public List<Worky.Project.Task.TaskStatus> TaskStatuses { get; set; }

        [DisplayName("Статус")]
        public int CurTaskStatusId { get; set; }
        public EditTaskModel()
        {
            GetTaskStatus();
        }
        public EditTaskModel(Worky.Project.Task.Task t)
        {
            this.TaskId = t.Id;
            this.Name = t.Name;
            this.Description = t.Description;
            this.Start = t.Start;
            this.End = t.End;
            this.CreationTime = t.CreationTime;
            this.ProjectId = t.ProjectId;
            this.TaskId = t.TaskStatusId;
            GetTaskStatus();
            
        }

        public void GetTaskStatus()
        {
            Data.Project.IProjectCollection col = new Data.Project.ProjectDbContext();
            this.TaskStatuses = col.GetTaskStatuses(ProjectId);

        }
        public SelectList GetTaskList()
        {
            return new SelectList(this.TaskStatuses, "Id", "Name", this.TaskStatuses.Where(i => i.Id == this.TaskId).FirstOrDefault());
        }
        


    }
}
