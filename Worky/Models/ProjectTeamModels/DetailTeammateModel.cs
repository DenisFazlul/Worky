namespace Worky.Models.ProjectTeamModels
{
    public class DetailTeammateModel
    {
        public int InviteId
        {
            get
            {
                return this.Invite.Id;
            }
        }
        public bool InviteConfirmed
        {
            get
            {
                return this.Invite.InviteAcsepted;
            }
        }
        public Users.User User { get; set; }
        public Worky.Project.Invite Invite { get; set; }

        public void SetUser(Users.User user)
        {
            this.User= user;
        }
        public void SetInvite(Worky.Project.Invite invite)
        {
            
            this.Invite= invite;
        }
    }
}
