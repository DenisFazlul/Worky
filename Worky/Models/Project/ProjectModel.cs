namespace Worky.Models.Project
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Worky.Project.Invite> Invites { get; set; }
        public ProjectModel()
        {
            this.Invites = new List<Worky.Project.Invite>();
        }

        internal void SetValuesFromProject(Worky.Project.Project model)
        {
            this.Id = model.Id;
            this.Name = model.Name;
        }
    }
}
