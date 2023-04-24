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
                return Content("no exist");
            }
            
            DocumentationModel model = new DocumentationModel();
            
            List<DocumentPage> pages=docDB.GetPagesForbook(bookId);
            List<PageModel> PagesModels = PageModelConverter.Convert(pages);

            PagesModelsSorter sorter = new PagesModelsSorter();
            
            sorter.Sort(PagesModels);


            model.BookModel = new BookModel();
            model.BookModel.Id = bookId;
            model.BookModel.StartPage = sorter.BasePage;

            if(selectedPage!=0)
            {
                PageModel sel=PagesModels.Where(i=>i.Id==selectedPage).FirstOrDefault();
                sel.IsSelected = true;
                model.SelectedPage = new SelectedPageModel();

                DocumentPage selectedDocPage=pages.Where(i=>i.Id==selectedPage).FirstOrDefault();
                model.SelectedPage.SetFromPage(selectedDocPage);
                
                
            }
            


            return View(model);

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



            return RedirectToAction("Watch", new { bookId = bookId});
        }
        public IActionResult AddBasePage(int bookId)
        {
            DocumentPage nPAge = new DocumentPage();
            nPAge.BookId = bookId;
            nPAge.ParrentId = 0;
            nPAge.Name = "BasePage";
            docDB.AddPage(nPAge);
            return RedirectToAction("Watch", new { bookId = bookId });
        }
         
        public IActionResult AddPage(int ParrentPageId)
        {
            int bookId = 0;


            DocumentPage parrent= docDB.GetPage(ParrentPageId);
            DocumentPage nPage = new DocumentPage();
            nPage.ParrentId = parrent.Id;
            nPage.BookId = parrent.BookId;
            docDB.AddPage(nPage);
            


            return RedirectToAction("Watch",new {bookId= parrent.BookId, selectePageId = nPage.Id});
        }
    }
}
