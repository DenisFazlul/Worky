using System.ComponentModel.DataAnnotations.Schema;

namespace Worky.Project
{
    public class Project
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public virtual  List<Note> Notes { get; set; }
        public List<Worky.Project.Task.TaskStatus > TaskStatuses { get; set; }
        public Project()
        {
           
        }
        public void GetNotes()
        {
            Data.Project.IProjectDb con = Data.DB.GetProject();
            this.Notes = con.GetNotes(this.Id);
        }

        internal void AddDefultTaskStatus()
        {
            Data.Project.IProjectDb con = Data.DB.GetProject();

            foreach (Worky.Project.Task.TaskStatus status in GetDefaultStatus())
            {
                con.AddTaskStatus(status);
            }
            
            
        }

        private IEnumerable<Task.TaskStatus> GetDefaultStatus()
        {
            List<Task.TaskStatus> st=new List<Task.TaskStatus>();
            Task.TaskStatus s1 = new Worky.Project.Task.TaskStatus();
            s1.ProjectId = this.Id;
            s1.Name = "BackLog";

            st.Add(s1);

            Task.TaskStatus s2 = new Worky.Project.Task.TaskStatus();
            s2.ProjectId = this.Id;
            s2.Name = "On work";
            st.Add(s2);
            Task.TaskStatus s3 = new Worky.Project.Task.TaskStatus();
            s3.ProjectId = this.Id;
            s3.Name = "Done";
            st.Add(s3);
            return st;



        }

        internal void AddNote(Note note)
        {
            Data.Project.IProjectDb con = Data.DB.GetProject();
            note.ProjectId = this.Id;
            con.AddNote(note);
        }
    }
}
