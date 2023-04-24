using Worky.Data.Project;
using Worky.Models.DocModels;
using Worky.Project.Documents;

namespace Worky.Services
{
    
    public class MemoryBookCache:IBookCashe
    {
        IDocmentationDB docDB;
        public static List<BookModel> bookModelsCache = new List<BookModel>();
        

        public void SetDb(IDocmentationDB db)
        {
            docDB = db;
        }
        public BookModel GetBookMode(int bookId)
        {
            BookModel exist = bookModelsCache.Where(i => i.Id == bookId).FirstOrDefault();
            if (exist == null)
            {
                exist = Create(bookId);

                bookModelsCache.Add(exist);

            }
            return exist;
        }
        public BookModel Create(int bookId)
        {
            BookModel exist = new BookModel();

            List<DocumentPage> pages = docDB.GetPagesForbook(bookId);
            exist.AllPages = PageModelConverter.Convert(pages);

            PagesModelsSorter sorter = new PagesModelsSorter();

            sorter.Sort(exist.AllPages);

            exist.StartPage = sorter.BasePage;
            exist.Id = bookId;
            return exist;
        }
        public void UpdateInCache(int bookId)
        {
            BookModel exist = bookModelsCache.Where(i => i.Id == bookId).FirstOrDefault();
            bookModelsCache.Remove(exist);
            BookModel nBm = Create(exist.Id);
            bookModelsCache.Add(nBm);
        }

       
    }
}
