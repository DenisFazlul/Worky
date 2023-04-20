using Worky.Project.Documents;

namespace Worky.Data.Project
{
    public interface IDocumentCollection
    {
        void AddDocument(Document doc);
        Document GetDocById(int id);

    }
}
