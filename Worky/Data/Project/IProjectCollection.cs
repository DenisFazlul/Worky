using Worky.Project;
namespace Worky.Data.Project
{
    public interface IProjectCollection
    {
        public Worky.Project.Project GetProject(int Id);
        public List<Worky.Project.Project> GetProjects();
        public void AddProject(Worky.Project.Project p);
        public void DeleteProject(Worky.Project.Project p);
        public void Update(Worky.Project.Project p);
        public List<Worky.Project.Project> GetProjectsForUser(int userId);
    }
}
