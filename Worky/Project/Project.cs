using System.ComponentModel.DataAnnotations.Schema;

namespace Worky.Project
{
    public class Project
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public virtual  List<Note> Notes { get; set; }
        public List<Worky.Project.Task.TaskStatus > TaskStatuses { get; set; }
        public Project()
        {
           
        }
         

        

        

        
    }
}
