namespace Worky.Models
{
    public class AddTaskFileModel
    {
        public int Id { get; set; }
        public IFormFile Path { get; set; }
        public int TaskId { get;  set; }
    }
}
