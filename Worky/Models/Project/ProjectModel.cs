namespace Worky.Models.Project
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public List<Worky.Project.Invite> Invites { get; set; }
        public ProjectModel()
        {
            this.Invites = new List<Worky.Project.Invite>();
        }
    }
}
