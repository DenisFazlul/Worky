using Worky.Project;

namespace Worky.Models
{
    public class InviteModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int ProjectId { get; set; }
        public Users.User User { get; set; }

         
    }
}
