using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Worky.Models
{
    public class CreateTaskModel
    {
        
        public Worky.Project.Task.Task Task { get; set; }
        public string BackLink { get; set; }

        [BindProperty]
        public int ProjectId { get; set; }
        public int TaskId { get; set; }

        [Required(ErrorMessage = "Введите наименование")]
        [DisplayName("Наименование")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите описание")]
        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Дата начала")]
        public DateTime Start { get; set; }
        [DisplayName("Дата кончания")]
        public DateTime End { get; set; }
        public DateTime CreationTime { get; set; }
        public List<Worky.Project.Task.TaskStatus> TaskStatuses { get; set; }
        public CreateTaskModel()
        {

        }

        internal Worky.Project.Task.Task GetTask(Data.Project.IProjectDb projectDB)
        {
            Worky.Project.Task.Task t = new Worky.Project.Task.Task();
            t.Name = this.Name;
            t.Description = this.Description;
            t.Start = this.Start;
            t.End = this.End;
            t.CreationTime = DateTime.Now;
            t.ProjectId = this.ProjectId;
            GetTaskStatus(projectDB);
            t.TaskStatusId = this.TaskStatuses[0].Id;


            return t;
        }
        public void GetTaskStatus(Data.Project.IProjectDb db)
        {
          
            this.TaskStatuses = db.GetTaskStatuses(ProjectId);

        }
        public SelectList GetTaskList()
        {
            return new SelectList(this.TaskStatuses, "Id", "Name", this.TaskStatuses.Where(i => i.Id == this.TaskId).FirstOrDefault());
        }


    }
}
