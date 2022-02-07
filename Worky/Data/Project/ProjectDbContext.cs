using Microsoft.EntityFrameworkCore;

namespace Worky.Data.Project
{
    public class ProjectDbContext : DbContext, IProjectCollection
    {
      public  DbSet<Worky.Project.Project> Projects { get; set; }
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
         : base(options)
        {
            Database.EnsureCreated();
        }
        public void AddProject(Worky.Project.Project p)
        {
            Projects.Add(p);
            this.SaveChanges();
        }

        public void DeleteProject(Worky.Project.Project p)
        {
            Worky.Project.Project project= this.Projects.Where(i => i.Id ==p.Id).FirstOrDefault();
            this.Projects.Remove(project);
            this.SaveChanges();
        }

        public Worky.Project.Project GetProject(int Id)
        {
            return this.Projects.Where(i => i.Id == Id).FirstOrDefault();
        }

        public List<Worky.Project.Project> GetProjects()
        {
            throw new NotImplementedException();
        }

        public List<Worky.Project.Project> GetProjectsForUser(int userId)
        {

            return this.Projects.Where(i => i.UserId == userId).ToList();
        }

        public void Update(Worky.Project.Project p)
        {
            Worky.Project.Project project = this.Projects.Where(i => i.Id == p.Id).FirstOrDefault();
            project.Name = p.Name;
            project.Description = p.Description;
            this.SaveChanges();
        }
    }
}
