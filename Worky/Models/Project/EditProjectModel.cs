namespace Worky.Models.Project
{
    public class EditProjectModel
    {
        public EditProjectModel(Worky.Project.Project project)
        {
            this.PorjecId = project.Id;
            this.Name = project.Name;
            this.Description = project.Description;
        }
        public EditProjectModel()
        {

        }
        public void SetSettingsToPrj(Worky.Project.Project p)
        {
            p.Name = this.Name;
            p.Description = this.Description;
        }
        public int PorjecId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
