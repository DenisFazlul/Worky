using Worky.Project.Documents;
namespace Worky.Data.Project
{
    public interface IDocmentationDB
    {
       
        public DocumentationBook GetBookByid(int PRojectId);
        public List<DocumentPage> GetPagesForbook(int BookId);
        public DocumentationBook GetBookFroProject(int ProjectId);
        public DocumentationBook CreateDocBookForProject(int ProjectId);
        public void UpdateDocBookSheme(int bookId,string json);
        public void RemoveDocBook(int id);

        public DocumentPage AddPage(DocumentPage page);
        public void RemovePage(int PageId);
        public DocumentPage GetPage(int id);
        void UpdatePage(DocumentPage page);



        public PageContent AddPageContent(int PageId);
        public void DeletePageContent(int PageContentId);
        
        public void UpdatePageContent(PageContent pageContent);
        public PageContent[] GetPageContentsForPage(int PageId);
        public PageContent GetPageContentById(int PageContentId);

    }
}
