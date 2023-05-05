using Worky.Project;
using Worky.Users;

namespace Worky.Models.Project
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User Owner { get; set; }
        public List<InviteModel> Invites { get; set; }
        public bool AllowAddInvites { get; internal set; }=false;

        public ProjectModel(Worky.Project.Project project)
        {
            this.Id = project.Id;
            this.Name = project.Name;
            this.Description = project.Description;
            this.Invites = new List<InviteModel>();
         
        }

        internal void SetValuesFromProject(Worky.Project.Project model)
        {
            this.Id = model.Id;
            this.Name = model.Name;
        }

         
    }
}
