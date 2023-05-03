using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Worky.Models.TaskTags;
using Worky.Project.Tags;
using Worky.Project.Task;

namespace Worky.Models.Project
{
    public class EditTaskModel
    {
        public Worky.Project.Task.Task Task { get; set; }
        public string BackLink { get; set; }
        public int ProjectId { get; set; }
        public int TaskId { get; set; }


        [DisplayName("Наименование")]
        public string Name { get; set; }


        [DisplayName("Описание")]
        [Required]
        public string Description { get; set; }

        [DisplayName("Дата начала")]
        public DateTime Start { get; set; }
        [DisplayName("Дата кончания")]
        public DateTime End { get; set; }
        public DateTime CreationTime { get; set; }
        public List<Worky.Project.Task.TaskStatus> TaskStatuses { get; set; }
        public List<CommentModel> CommentModels { get; private set; }
        public CommentModel EditComment { get; set; }

        [DisplayName("Статус")]
        public int CurTaskStatusId { get; set; }
        public Users.User CurUser { get; private set; }
        public Users.User Owner { get; set; }
        public List<TagTaskInstanceModel> Tags { get; private set; }

        internal void AddTag(TagTaskInstanceModel  model)
        {
            Tags.Add(model);
        }

        public EditTaskModel()
        {
            this.Tags = new List<TagTaskInstanceModel>();
            this.CommentModels = new List<CommentModel>();
        }

        internal void SetCommentsModel(List<CommentModel> comments)
        {
            this.CommentModels = comments;
        }

        public EditTaskModel(Worky.Project.Task.Task t)
        {
            this.Tags = new List<TagTaskInstanceModel>();
            this.CommentModels = new List<CommentModel>();
            this.Task = t;
            this.TaskId = t.Id;
            this.Name = t.Name;
            this.Description = t.Description;
            this.Start = t.Start;
            this.End = t.End;
            this.CreationTime = t.CreationTime;
            this.ProjectId = t.ProjectId; 
            
        }

        

        private void CreateEditComment()
        {
            this.EditComment = new CommentModel();
            this.EditComment.TaskId = this.TaskId;
          
            this.EditComment.UserId = this.CurUser.Id;


        }
        public void SetUser(Users.User user)
        {
            this.CurUser = user;
            CreateEditComment();
        }
        public string GetOwnerName()
        {
            string val = "Не существует";
            if (this.Owner != null)
            {
                val = this.Owner.GetName();
            }
            return val;
        }

        

        
        public SelectList GetTaskList()
        {
            return new SelectList(this.TaskStatuses, "Id", "Name", this.TaskStatuses.Where(i => i.Id == this.TaskId).FirstOrDefault());
        }
        


    }
}
