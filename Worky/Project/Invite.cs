using System.ComponentModel.DataAnnotations.Schema;

namespace Worky.Project
{
    public class Invite
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        [NotMapped]
        public string Name { get; set; }
    }
}
