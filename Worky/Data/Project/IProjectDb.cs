using Worky.Models.Project;
using Worky.Project;
using Worky.Project.Documents;
using Worky.Project.Task;

namespace Worky.Data.Project
{
    public interface IProjectDb
    {
        public Worky.Project.Project GetProject(int Id);
        public List<Worky.Project.Project> GetProjects();
        public void AddProject(Worky.Project.Project p);
        public void DeleteProject(Worky.Project.Project p);
        Worky.Project.Task.Task GetTaskById(int taskId);
       List<Worky.Project.Task.Task> GetTaskByDay(DateTime date,int ProjectId);
        List<DocIerarhy> GetPagesForProject(int pid);
        public void Update(Worky.Project.Project p);
        List<TaskComment> GetTaskCommentsByTaskId(int id);
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
        void AddCommentToTask(TaskComment com);
        List<Worky.Project.Task.Task> GetTasksForProject(int projectId);
        void AddTaskFile(TaskFile ts);
        void RemoveTaskFile(TaskFile ts);

        List<TaskFile> GetTaskFiles(int TaskId);
        TaskFile GetTaskFileById(int TaskFileId);
    }
}
