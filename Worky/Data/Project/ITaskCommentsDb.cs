using Worky.Project.Task;

namespace Worky.Data.Project
{
    public interface ITaskCommentsDb
    {
        List<TaskComment> GetTaskCommentsByTaskId(int id);

        void AddCommentToTask(TaskComment com);
        void RemoveComment(TaskComment tc);

    }
}
