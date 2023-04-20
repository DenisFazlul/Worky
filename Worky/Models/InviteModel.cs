using Worky.Project;

namespace Worky.Models
{
    public class InviteModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int ProjectId { get; set; }
        public Users.User User { get; set; }

        internal void SetIvite(Invite invite)
        {
            this.Id = invite.Id;
            Users.IUsersCollection col= Data.DB.GetProject() as Worky.Users.IUsersCollection;
            User = col.GetUser(invite.UserId);

        }
    }
}
