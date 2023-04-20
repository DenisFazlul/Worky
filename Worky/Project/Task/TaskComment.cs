using System.ComponentModel.DataAnnotations.Schema;

namespace Worky.Project.Task
{
    public class TaskComment
    {
        public int Id { get; set; }
        public int TaskId { get; set; }

        public string Comment { get; set; }
        public int UserId { get; set; }
        public DateTime DateTime { get; set; }

        internal void Delete()
        {
            Worky.Data.Project.ITaskCommentsDb com = Data.DB.GetCommntsDb();
            com.RemoveComment(this);
        }
    }
        
}
