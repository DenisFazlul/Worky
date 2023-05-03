using Worky.Users;

namespace Worky.Data
{
    public class DB
    {
        public static Data.Project.IdFilesDb GetFileDb()
        {
            Data.Project.IdFilesDb col = new Data.Project.ProjectDbContext();
            return col;
        }
        public static Data.Project.ITaskCommentsDb GetCommntsDb()
        {
            Data.Project.ITaskCommentsDb com = new Data.Project.ProjectDbContext();
            return com;
        }
        public static Data.Project.IProjectDb GetProject()
        {
            Data.Project.IProjectDb col = new Data.Project.ProjectDbContext();
            return col;
        }

       
    }
}
