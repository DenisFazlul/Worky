using Worky.Project;

namespace Worky.Models.Project
{
    public class TaskFile
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        
        public int FileId { get; set; }
        public string Name { get; set; }
        public TaskFile()
        {

        }
 

        
    }
}
