using Microsoft.EntityFrameworkCore;
using Worky.Models.Project;
using Worky.Project;
using Worky.Project.Documents;
using Worky.Project.Task;
using Worky.Users;

namespace Worky.Data.Project
{
    ///Add-Migration t2 -context ProjectDbContext

    ///Update-Database -context ProjectDbContext
    public class ProjectDbContext : DbContext, IProjectDb, IdFilesDb, ITaskCommentsDb, IUsersCollection, IIviteCollection, IDocmentationDB
    {
       public DbSet<DocumentPage> Pages { get; set; }
        public DbSet<DocumentationBook> DocumentationBooks { get; set; }
        public DbSet<Worky.Project.Note> Notes { get; set; }
        public DbSet<Worky.Project.Project> Projects { get; set; }
        public DbSet<Worky.Project.Task.Task> Tasks { get; set; }
        public DbSet<Worky.Project.Task.TaskComment> TaskComments { get; set; }
        public DbSet<Worky.Project.Task.TaskStatus> TaskStatuses { get; set; }
        public DbSet<TaskFile> TaskFile { get; set; }
        public DbSet<DFile> DFiles { get; set; }
        public DbSet<Worky.Project.Task.TimeData> TimeDatas { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Invite> invites { get; set; }

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
         : base(options)
        {
            Database.EnsureCreated();


        }
        public ProjectDbContext()
        {
            Database.EnsureCreated();
        }
        public List<Worky.Project.Note> GetNotes(int ProjectId)
        {
            return this.Notes.Where(i => i.ProjectId == ProjectId).ToList();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Worky.DataDB.connectionStrinProjectDb);


        }
        public void AddProject(Worky.Project.Project p)
        {
            Projects.Add(p);
            this.SaveChanges();
        }

        public void DeleteProject(Worky.Project.Project p)
        {
            Worky.Project.Project project = this.Projects.Where(i => i.Id == p.Id).FirstOrDefault();
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
            List<Worky.Project.Project> prjs = this.Projects.ToList();
            List<Worky.Project.Project> filterePrjs = prjs.Where(i => i.UserId == userId).ToList();
            return filterePrjs;
        }

        public void Update(Worky.Project.Project p)
        {
            Worky.Project.Project project = this.Projects.Where(i => i.Id == p.Id).FirstOrDefault();
            project.Name = p.Name;
            project.Description = p.Description;
            project.Notes = p.Notes;

            this.SaveChanges();
        }
        public void AddNote(Worky.Project.Project p, Worky.Project.Note n)
        {
            //this.Notes.Add(n);
            //Worky.Project.Project project = GetProject(p.Id);
            //project.
        }
        public void Save()
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
            Note n = this.Notes.Where(i => i.Id == note.Id).FirstOrDefault();

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
            List<Worky.Project.Task.TaskStatus> st = this.TaskStatuses.Where(i => i.ProjectId == id).ToList();


            return st;
        }

        public void AddTaskStatus(Worky.Project.Task.TaskStatus status)
        {
            this.TaskStatuses.Add(status);
            Save();
        }

        public Worky.Project.Task.TaskStatus GetTaskStatusById(int taskStatusId)
        {
            Worky.Project.Task.TaskStatus ExistTS = this.TaskStatuses.Where(i => i.Id == taskStatusId).FirstOrDefault();

            return ExistTS;
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
            foreach (Worky.Project.Task.Task t in tasks)
            {
                t.Delete();

            }


            this.Save();
        }

        public void Update(Worky.Project.Task.Task task)
        {
            Worky.Project.Task.Task t = this.Tasks.Where(i => i.Id == task.Id).FirstOrDefault();
            t.SetData(task);
            Save();
        }

        public void DeleteTask(Worky.Project.Task.Task task)
        {
            this.Tasks.Remove(this.Tasks.Where(i => i.Id == task.Id).FirstOrDefault());
            Save();
        }







        public List<Worky.Project.Task.Task> GetTaskByDay(DateTime date, int ProjectId)
        {
            return this.Tasks.Where(i => i.End.Date >= date && i.Start <= date && i.ProjectId == ProjectId).ToList();
        }

        public List<Worky.Project.Task.Task> GetTasksForProject(int projectId)
        {
            return this.Tasks.Where(i => i.ProjectId == projectId).ToList();
        }

        public List<TaskComment> GetTaskCommentsByTaskId(int id)
        {
            return this.TaskComments.Where(i => i.TaskId == id).OrderBy(i => i.DateTime).ToList();
        }

        public void AddCommentToTask(TaskComment com)
        {
            this.TaskComments.Add(com);
            Save();
        }

        public void AddTaskFile(TaskFile ts)
        {
            this.TaskFile.Add(ts);
            this.SaveChanges();
        }

        public void RemoveTaskFile(TaskFile ts)
        {
            TaskFile exist = this.TaskFile.Where(i => i.Id == ts.Id).FirstOrDefault();
            if (exist != null)
            {
                this.TaskFile.Remove(exist);
                this.SaveChanges();
            }
        }

        public void AddFile(DFile dFile)
        {
            this.DFiles.Add(dFile);
            this.SaveChanges();
        }

        public void Remove(DFile dFile)
        {
            DFile exist = this.DFiles.Where(i => i.Id == dFile.Id).FirstOrDefault();
            if (exist != null)
            {
                this.DFiles.Remove(exist);
                this.SaveChanges();
            }
        }

        public DFile GetById(int id)
        {
            return this.DFiles.Where(i => i.Id == id).FirstOrDefault();
        }

        public List<TaskFile> GetTaskFiles(int TaskId)
        {
            return this.TaskFile.Where(i => i.TaskId == TaskId).ToList();
        }

        public TaskFile GetTaskFileById(int TaskFileId)
        {
            return this.TaskFile.Where(i => i.Id == TaskFileId).FirstOrDefault();
        }

        public void RemoveComment(TaskComment tc)
        {
            TaskComment exist = this.TaskComments.Where(i => i.Id == tc.Id).FirstOrDefault();
            if (exist != null)
            {
                this.TaskComments.Remove(exist);
                this.SaveChanges();
            }
        }
        public void AddUser(User user)
        {
            user.GenerateCode();
            this.Users.Add(user);
            this.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            User u = Users.Where(i => i.Id == user.Id).FirstOrDefault();
            Users.Remove(u);
            this.SaveChanges();
        }

        public User GetUser(int id)
        {
            return this.Users.Where(i => i.Id == id).FirstOrDefault();
        }

        public User GetUser(string Email)
        {


            return this.Users.Where(i => i.Email == Email).FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            return this.Users.ToList();
        }



        public void UpdateUser(User user)
        {
            User existUser = Users.Where(i => i.Id == user.Id).FirstOrDefault();
            existUser.Pass = user.Pass;
            existUser.IsConfirmed = user.IsConfirmed;
            existUser.UserName = user.UserName;
            existUser.Email = user.Email;
            existUser.IsBlock = user.IsBlock;
            this.SaveChanges();
        }
        public void AddInvite(Invite invite)
        {
            invites.Add(invite);
            this.SaveChanges();
        }

        public void Delete(Invite invite)
        {
            this.invites.Remove(invites.Where(i => i.Id == invite.Id).FirstOrDefault());
            this.SaveChanges();
        }
        public void RemoveProjectInvites(int ProijectId)
        {
            foreach (Invite i in invites.Where(i => i.ProjectId == ProijectId))
            {
                Delete(i);
            }
        }

        public List<Invite> GetInvites(int UserId)
        {
            return this.invites.Where(i => i.UserId == UserId).ToList();
        }

        public List<Invite> GetInvitesForProject(int id)
        {
            return this.invites.Where(i => i.ProjectId == id).ToList();
        }

        public Invite GetInvite(int Id)
        {
            return this.invites.Where(i => i.Id == Id).FirstOrDefault();
        }


        #region DocDB
        public DocumentationBook GetBookByid(int booKId)
        {
            DocumentationBook book = null; 
            book=this.DocumentationBooks.FirstOrDefault(i => i.Id == booKId);
            return book;
            //throw new NotImplementedException();
        }
        public DocumentationBook GetBookFroProject(int ProjectId)
        {
            DocumentationBook book = null;
            book = this.DocumentationBooks.FirstOrDefault(i => i.ProjectId == ProjectId);
            return book;
        }

        public DocumentationBook CreateDocBookForProject(int ProjectId)
        {
            DocumentationBook book = null;
            book = new DocumentationBook();
            book.ProjectId= ProjectId;
            book.Json = "";
            DocumentationBooks.Add(book);
            this.SaveChanges();
            DocumentPage basePage = new DocumentPage();
            basePage.ParrentId = 0;
            basePage.BookId = book.Id;
           
            this.Pages.Add(basePage);

            this.SaveChanges();

            return book;
        }

        public void UpdateDocBookSheme(int id, string json)
        {
            DocumentationBook exist = null;
             exist=   DocumentationBooks.Where(i=>i.Id==id).FirstOrDefault();
            exist.Json = json;
            this.SaveChanges();
        }
        

        public void RemoveDocBook(int id)
        {
            DocumentationBook exist = null;
            exist = DocumentationBooks.Where(i => i.Id == id).FirstOrDefault();
            DocumentationBooks.Remove(exist);
            this.SaveChanges();
        }

        public DocumentPage AddPage(DocumentPage page)
        {
           
            this.Pages.Add(page);
            this.SaveChanges();

            return page;
        }
        public DocumentPage GetPage(int id)
        {
            DocumentPage page = null;
            page = this.Pages.FirstOrDefault(i => i.Id == id);
            return page;
        }

        public void RemovePage(int PageId)
        {
            DocumentPage exist=GetPage(PageId);
            this.Pages.Remove(exist);
            this.SaveChanges();
        }
        public List<DocumentPage> GetPagesForbook(int BookId)
        {
            return this.Pages.Where(i => i.BookId == BookId).ToList();
        }

        public void UpdatePage(DocumentPage page)
        {
            DocumentPage exist = GetPage(page.Id);
            exist.Name = page.Name;
            exist.Description = page.Description;
            this.SaveChanges();
        }
        #endregion
    }
}
