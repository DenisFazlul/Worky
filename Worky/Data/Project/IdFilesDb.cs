using Worky.Project;

namespace Worky.Data.Project
{
    public interface IdFilesDb
    {
        void AddFile(DFile dFile);
        void Remove(DFile dFile);

        DFile GetById(int id);
        
    }
}
