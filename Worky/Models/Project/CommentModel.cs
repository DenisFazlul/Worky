using Worky.Project.Task;

namespace Worky.Models.Project
{
    public class CommentModel
    {

       
        public int UserId { get; internal set; }
        public string Comment { get; set; }
        public int TaskId { get; internal set; }
        
        public string UserName { get; set; }
        public Users.User User { get; set; }
        public string DateTime { get; set; }
        public CommentModel()
        {

        }
        public CommentModel(TaskComment com)
        {
            this.Comment = com.Comment;
            this.UserId = com.UserId;
            this.TaskId = com.TaskId;
     
            this.Comment = com.Comment;
           
            this.DateTime = com.DateTime.ToString();
        }
        public string GetuserName()
        {
            if(this.User==null)
            {
                return "Пользователь удалён";
            }
            else
            {
                return this.User.GetName();
            }
        }

    
    }
}
