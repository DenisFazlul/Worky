using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Worky.Project.Task
{
    public class TimeData
    {
        [Key]
        [ForeignKey("Task")]
        public int Id { get; set; }
        public Task Task { get; set; }
        public DateTime End { get; set; }
        public DateTime Start { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
