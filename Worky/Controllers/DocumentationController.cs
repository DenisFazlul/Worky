using Microsoft.AspNetCore.Mvc;
using Worky.Data.Project;
using Worky.Project.Documents;
using Worky.Models.DocModels;
namespace Worky.Controllers
{
    public class DocumentationController : Controller
    {
        IDocmentationDB docDB;
        public DocumentationController(ProjectDbContext db)
        {
            docDB = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProjDoc(int ProjectId)
        {
            
            DocumentationBook book = docDB.GetBookFroProject(ProjectId);
         
            
            if(book==null)
            {
                book = docDB.CreateDocBookForProject(ProjectId);
               
                
            }

            List<DocumentPage> pages = docDB.GetPagesForbook(book.Id);
           

            return RedirectToAction("Watch",new { bookId = book.Id });
        }
       
        public IActionResult Watch(int bookId,int selectedPage=0)
        {
            DocumentationBook exist= docDB.GetBookByid(bookId);
            if (exist == null)
            {
                Models.Message msg = new Models.Message();

                msg.Content = "Документации не существует";
                msg.BackUrl = "/";
                msg.Header = "Не найден";
                msg.UrlName = "К проектам";
                return View("Message", msg);
               
            }
            

            DocumentationModel model = new DocumentationModel();
            
            

            model.BookModel = GetBookMode(bookId);
          
            List<Task> tasks = new List<Task>();
            if(selectedPage!=0)
            {

                Task t1= Task.Factory.StartNew(() =>
                {
                    model.BookModel.SetSelectedModel(selectedPage);
                });
                tasks.Add(t1);
                Task t2= Task.Factory.StartNew(() =>
                {
                    DocumentPage selectedDocPage = docDB.GetPage(selectedPage);
                    model.SelectedPage = new SelectedPageModel();
                    model.SelectedPage.SetFromPage(selectedDocPage);
                });
                tasks.Add(t2);

                Task t3 = Task.Factory.StartNew(() => 
                { 
                    ///loading docs for selected page
                });
 
                
                
            }


            Task.WaitAll(tasks.ToArray());
            return View(model);

        }
        public static List<BookModel> bookModelsCache = new List<BookModel>();

        
        public BookModel GetBookMode(int bookId)
        {
            BookModel exist = bookModelsCache.Where(i => i.Id == bookId).FirstOrDefault();
            if(exist==null)
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
            BookModel exist=bookModelsCache.Where(i=>i.Id==bookId).FirstOrDefault();
            bookModelsCache.Remove(exist);
            BookModel nBm = Create(exist.Id);
            bookModelsCache.Add(nBm);
        }
        
       
        public IActionResult RemovePage(int PageId)
        {
            DocumentPage existPage = docDB.GetPage(PageId);
            int bookId = existPage.BookId;

            List<DocumentPage> pages = docDB.GetPagesForbook(bookId);
            List<PageModel> PagesModels = PageModelConverter.Convert(pages);

            PagesModelsSorter sorter = new PagesModelsSorter();

            sorter.Sort(PagesModels);
            PageModel basePage = PagesModels.Where(i => i.Id == PageId).FirstOrDefault();
            List<int> reomveIds= basePage.GetChildsIds();
            reomveIds.Add(PageId);
            foreach(int reomveId in reomveIds)
            {
                docDB.RemovePage(reomveId);
            }
            // docDB.RemovePage(PageId);

            UpdateInCache(bookId);

            return RedirectToAction("Watch", new { bookId = bookId});
        }
        public IActionResult AddBasePage(int bookId)
        {
            DocumentPage nPAge = new DocumentPage();
            nPAge.BookId = bookId;
            nPAge.ParrentId = 0;
            nPAge.Name = "BasePage";
            docDB.AddPage(nPAge);
            return RedirectToAction("Watch", new { bookId = bookId, selectedPage=nPAge.Id });
        }
         
        public IActionResult AddPage(int ParrentPageId)
        {
            


            DocumentPage parrent= docDB.GetPage(ParrentPageId);
            List<DocumentPage> pages = docDB.GetPagesForbook(parrent.BookId);

            DocumentPage nPage = new DocumentPage();
            nPage.Name = $"New Page ({pages.Count+1})";
            nPage.ParrentId = parrent.Id;
            nPage.BookId = parrent.BookId;
            docDB.AddPage(nPage);

            UpdateInCache(parrent.BookId);

            return RedirectToAction("Watch",new {bookId= parrent.BookId, selectedPage = nPage.Id});
        }

        
    }
}
