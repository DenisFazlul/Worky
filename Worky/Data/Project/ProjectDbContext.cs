using Microsoft.EntityFrameworkCore;
using Worky.Project;
using Worky.Project.Documents;

namespace Worky.Data.Project
{
    ///Add-Migration Pages -context ProjectDbContext

    ///Update-Database -context ProjectDbContext
    public class ProjectDbContext : DbContext, IProjectCollection
    {
       public DbSet<Worky.Project.Documents.DocIerarhy> DocIerarhies { get; set; }
        public DbSet<Worky.Project.Documents.Document> Documents { get; set; }
        public DbSet<Worky.Project.Note> Notes { get; set; }
        public  DbSet<Worky.Project.Project> Projects { get; set; }
        public DbSet<Worky.Project.Task.Task> Tasks { get; set; }
        public DbSet<Worky.Project.Task.TaskStatus>TaskStatuses { get; set; }
    public DbSet<Worky.Project.Task.TimeData> TimeDatas { get; set; }

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
         : base(options)
        {
            Database.EnsureCreated();
            
          
        }
        public ProjectDbContext() 
        {
            
        }
        public List<Worky.Project.Note> GetNotes(int ProjectId)
        {
            return this.Notes.Where(i => i.ProjectId == ProjectId).ToList();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Worky.DataDB.connectionString);
            
            
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

            return this.Projects.FirstOrDefault(i => i.Id == Id);
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
            project.Notes = p.Notes;
           
            this.SaveChanges();
        }
        public void AddNote(Worky.Project.Project p , Worky.Project.Note n)
        {
            //this.Notes.Add(n);
            //Worky.Project.Project project = GetProject(p.Id);
            //project.
        }
        public void  Save()
        {
            this.SaveChanges();
        }

        public Note GetNote(int id)
        {
            return this.Notes.Where(i => i.Id == id).FirstOrDefault();
        }

        public void AddNote(Note note)
        {
            this.Notes.Add(note);
            Save();
        }

        public void RemoveNote(int notId)
        {
            Note n = this.Notes.Where(i => i.Id == notId).FirstOrDefault();
            this.Notes.Remove(n);
            Save();
        }

        public void Update(Note note)
        {
           Note n= this.Notes.Where(i => i.Id == note.Id).FirstOrDefault();

            n.SetData(note);
            Save();
        }

       
        public void AddTask(Worky.Project.Task.Task t)
        {
            
              this.Tasks.Add(t);
            
            Save();
        }

        public List<Worky.Project.Task.TaskStatus> GetTaskStatuses(int id)
        {
            List<Worky.Project.Task.TaskStatus> st= this.TaskStatuses.Where(i => i.ProjectId == id).ToList();
            foreach(Worky.Project.Task.TaskStatus s in st)
            {
                s.Tasks = this.Tasks.Where(i => i.TaskStatusId == s.Id)
                    
                    .ToList();
                
            }

            return st;
        }

        public void AddTaskStatus(Worky.Project.Task.TaskStatus status)
        {
            this.TaskStatuses.Add(status);
            Save();
        }

        public Worky.Project.Task.TaskStatus GetTaskStatusById(int taskStatusId)
        {
            return this.TaskStatuses.Where(i => i.Id == taskStatusId).FirstOrDefault();
        }

        public Worky.Project.Task.Task GetTaskById(int taskId)
        {
            return this.Tasks.Where(i => i.Id == taskId).FirstOrDefault();
        }

        public void Update(Worky.Project.Task.TaskStatus taskStatus)
        {
            Worky.Project.Task.TaskStatus st = this.TaskStatuses.Where(i => i.Id == taskStatus.Id).FirstOrDefault();
            st.Name = taskStatus.Name;
            Save();
        }

        public void RemoveTaskStatus(Worky.Project.Task.TaskStatus taskStatus)
        {
            Worky.Project.Task.TaskStatus st = this.TaskStatuses.Where(i => i.Id == taskStatus.Id).FirstOrDefault();
            this.TaskStatuses.Remove(st);
            List<Worky.Project.Task.Task> tasks = this.Tasks.Where(i => i.TaskStatusId == st.Id).ToList();
            foreach(Worky.Project.Task.Task t in tasks)
            {
                this.Tasks.Remove(t);
            }


            this.Save();
        }

        public void Update(Worky.Project.Task.Task task)
        {
           Worky.Project.Task.Task t = this.Tasks.Where(i=>i.Id==task.Id).FirstOrDefault();
            t.SetData(task);
            Save();
        }

        public void DeleteTask(Worky.Project.Task.Task task)
        {
            this.Tasks.Remove(this.Tasks.Where(i => i.Id == task.Id).FirstOrDefault());
            Save();
        }

        public List<DocIerarhy> GetPagesForProject(int pid)
        {
            List<DocIerarhy> irhs = new List<DocIerarhy>();
            foreach(DocIerarhy ir in this.DocIerarhies.Where(i => i.ProjectId == pid).ToList())
            {
                Document d= this.Documents.Where(i => i.Id == ir.Id).FirstOrDefault();
                if(d==null)
                {
                    d = new Document();
                    d.Name = "NewDoc";
                    this.Documents.Add(d);
                    Save();

                }
                ir.DocName = d.Name;
                irhs.Add(ir);
            }
            return irhs;
        }

        public void AddDocument(Document doc)
        {
            this.Documents.Add(doc);
            Save();
        }

        public void AddDocIerarhy(DocIerarhy ir)
        {
            this.DocIerarhies.Add(ir);
            Save();
        }

        public List<Worky.Project.Task.Task> GetTaskByDay(DateTime date)
        {
            return this.Tasks.Where(i => i.End.Date >= date&&i.Start<=date).ToList();
        }
    }
}
