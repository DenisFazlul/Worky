using Worky.Project.Documents;

namespace Worky.Data.Project
{
    public interface IPageContentType
    {
        public PageContentType[] GetPageContentTypes();
        public PageContentType GetPageContentTypeById(int id);

    }
}
