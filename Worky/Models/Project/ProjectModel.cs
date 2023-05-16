using Worky.Project;
using Worky.Users;

namespace Worky.Models.Project
{
    public class ProjectInsrutemst
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LinkToAction { get; set; }
    }
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User Owner { get; private set; }
        public List<InviteModel> Invites { get; set; }
        public bool AllowAddInvites { get; internal set; }=false;
        public List<ProjectInsrutemst> insrutemsts { get; set; }

        public ProjectModel()
        {
            insrutemsts = new List<ProjectInsrutemst>();
            
        }
        private void IntiLinks(int projectId)
        {
            insrutemsts = new List<ProjectInsrutemst>()
            {
                new ProjectInsrutemst(){
                    Name="Заметки",
                    Description="Заметки для работы в команнде, запишите то что нужно не забыть",
                    LinkToAction=$"/Notes/ProjectNotes?ProjectId={projectId}",
                },

                new ProjectInsrutemst(){
                    Name="Задачи",
                    Description="Доска для работы с задачами, создавайте таски, изменяйте их статус и взаимодействуйте с командой",
                    LinkToAction=$"/Tasks/ProjectTasks?ProjectId={projectId}",
                },

                new ProjectInsrutemst(){
                    Name="Документация",
                    Description="Ведите документацию проекта, пишите инструкции, гайды, и прочие материалы для работы с проектом",
                    LinkToAction=$"/Documentation/ProjDoc?ProjectId={projectId}",
                },
                new ProjectInsrutemst(){
                    Name="Каледарь",
                    Description="Календарь для визуального отображения задач, ослеживайте когда задачи должны быть выполнены",
                    LinkToAction=$"/Callendar/Month?pid={projectId}",
                },
                new ProjectInsrutemst(){
                    Name="Команда",
                    Description="Добавьте пользователей в комнду проекта, они смогут работать с вами в одном проекте",
                    LinkToAction=$"/ProjectTeam/Team?pid={projectId}",
                },



            };
        }
        public void SetOwner(User user)
        {
            this.Owner = user;
        }
        public void SetModelDataFromProject(Worky.Project.Project project)
        {
            this.Id = project.Id;
            this.Name = project.Name;
            this.Description = project.Description;
            this.Invites = new List<InviteModel>();
            IntiLinks(project.Id);

        }

       

         
    }
}
