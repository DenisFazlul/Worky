using System.ComponentModel.DataAnnotations.Schema;
using Worky.Data.Project;

namespace Worky.Project.Task
{
    public class TaskStatus
    {
        public int Id { get; set; }
        
        public int ProjectId { get; set; }
        public int SortIndex { get; set; }
        public string Name { get; set; } = "New";
        public string Description { get; set; } = "TemplDesk";
        [NotMapped]
        public List<Task> Tasks { get; set; }
        public TaskStatus()
        {
            this.Tasks = new List<Task>();
        }

      
        public static IEnumerable<Worky.Project.Task.TaskStatus> CreateDefultTaskSttusForProject(IProjectDb db, int projectID)
        {
            List<TaskStatus> st = new List< TaskStatus>();
            TaskStatus s1 = new Worky.Project.Task.TaskStatus();
            s1.ProjectId = projectID;
            s1.Name = "BackLog";

            st.Add(s1);

            TaskStatus s2 = new Worky.Project.Task.TaskStatus();
            s2.ProjectId = projectID;
            s2.Name = "On work";
            st.Add(s2);
            TaskStatus s3 = new Worky.Project.Task.TaskStatus();
            s3.ProjectId = projectID;
            s3.Name = "Done";
            st.Add(s3);

            foreach(TaskStatus t in st)
            {
                db.AddTaskStatus(t);
            }

            return st;



        }

    }
}
