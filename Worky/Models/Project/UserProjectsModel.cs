namespace Worky.Models.Project
{
    public class UserProjectsModel
    {
        public List<Models.Project.ProjectModel> UserProject { get; set; }
        public List<Models.Project.ProjectModel> InvitesProject { get; set; }

        internal List<Models.Project.ProjectModel> ConvertProjectsToModels(List<Worky.Project.Project> projects)
        {
            List<ProjectModel> models=new List<ProjectModel>();

            foreach(Worky.Project.Project prj in projects )
            {
                ProjectModel model = new ProjectModel(prj);
                models.Add(model);
            }
            return models;
        }
    }
}
