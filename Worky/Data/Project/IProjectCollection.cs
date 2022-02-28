using Worky.Project;
using Worky.Project.Documents;

namespace Worky.Data.Project
{
    public interface IProjectCollection
    {
        public Worky.Project.Project GetProject(int Id);
        public List<Worky.Project.Project> GetProjects();
        public void AddProject(Worky.Project.Project p);
        public void DeleteProject(Worky.Project.Project p);
        Worky.Project.Task.Task GetTaskById(int taskId);
       List<Worky.Project.Task.Task> GetTaskByDay(DateTime date);
        List<DocIerarhy> GetPagesForProject(int pid);
        public void Update(Worky.Project.Project p);
        public void Update(Note note);
        public void Update(Worky.Project.Task.Task task);
        public void Update(Worky.Project.Task.TaskStatus taskStatus);
        public List<Worky.Project.Project> GetProjectsForUser(int userId);
        public List<Worky.Project.Note> GetNotes(int ProjectId);
        void RemoveTaskStatus(Worky.Project.Task.TaskStatus ts);
        void AddDocument(Document doc);
        List<Worky.Project.Task.TaskStatus> GetTaskStatuses(int id);
        void AddDocIerarhy(DocIerarhy ir);
        public Note GetNote(int id);
        void AddNote(Note note);
        void RemoveNote(int notId);
        void AddTask(Worky.Project.Task.Task t);
        
        void AddTaskStatus(Worky.Project.Task.TaskStatus status);
        Worky.Project.Task.TaskStatus GetTaskStatusById(int taskStatusId);
        void DeleteTask(Worky.Project.Task.Task task);
    }
}
