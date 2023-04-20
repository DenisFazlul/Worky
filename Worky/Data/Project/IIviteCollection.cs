namespace Worky.Data
{
    public interface IIviteCollection
    {
        public void AddInvite(Worky.Project.Invite invite);
        public void Delete(Worky.Project.Invite invite);
        public List<Worky.Project.Invite> GetInvites(int UserId);
        public void RemoveProjectInvites(int ProijectId);
        public List<Worky.Project.Invite> GetInvitesForProject(int id);
        public Worky.Project.Invite GetInvite(int Id);
    }
}
