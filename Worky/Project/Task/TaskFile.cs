using Worky.Project;

namespace Worky.Models.Project
{
    public class TaskFile
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        
        public int FileId { get; set; }
        public string Name { get; set; }
        public TaskFile()
        {

        }

        internal void AddToDb()
        {
            Data.Project.IProjectDb prj = Data.DB.GetProject();
            prj.AddTaskFile(this);
        }

        internal void Delete()
        {
            Data.Project.IdFilesDb files = Data.DB.GetFileDb();
            Data.Project.IProjectDb col = Data.DB.GetProject();
            col.RemoveTaskFile(this);
            DFile dFile = files.GetById(this.FileId);
            dFile.Delete();
            
        }
    }
}
