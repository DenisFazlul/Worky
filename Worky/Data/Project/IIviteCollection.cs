using Worky.Project;

namespace Worky.Data
{
    public interface IIviteCollection
    {
        public void AddInvite(Worky.Project.Invite invite);
        public void DeleteInvite(int id);
        public List<Worky.Project.Invite> GetInvites(int UserId);
        public void RemoveProjectInvites(int ProijectId);
        public List<Worky.Project.Invite> GetInvitesForProject(int id);
        public Worky.Project.Invite GetInvite(int Id);
        public Invite[] GetInvitedForUser(int id);
        void Update(Invite invite);
    }
}
