using Microsoft.AspNetCore.Mvc;
using Worky.Data.Project;
using Worky.Project.Documents;
using Worky.Models.DocModels;
using Worky.Services;
using Worky.Users;

namespace Worky.Controllers
{
    public class DocumentationController : Controller
    {
        IDocmentationDB docDB;
        IBookCashe booksCache;
        IUsersCollection usersDb;
        Data.IIviteCollection invitesDb;
        IProjectDb projectDb;
        public DocumentationController(ProjectDbContext db,IBookCashe cash,IUsersCollection users)
        {
            docDB = db;
            booksCache = cash;
            booksCache.SetDb(db);
            usersDb = users;
            invitesDb = db;
            projectDb = db;
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
       
        public IActionResult Watch(int bookId,int selectedPageId=0)
        {
            DocumentationBook exist= docDB.GetBookByid(bookId);
            
            Users.User curUser = usersDb.GetUser(User.Identity.Name);
            if (exist == null)
            {
                Models.Message msg = new Models.Message();

                msg.Content = "Документации не существует";
                msg.BackUrl = "/";
                msg.Header = "Не найден";
                msg.UrlName = "К проектам";
                return View("Message", msg);
               
            }
            Project.Project prj = projectDb.GetProject(exist.ProjectId);
            Services.UserAcsessService ac = new Services.UserAcsessService(invitesDb, projectDb);
            if(ac.IsUserAccsessToProject(curUser,prj)==false)
            {
                Models.Message msg = new Models.Message();

                msg.Content = "Нет доступа";
                msg.BackUrl = "/";
                msg.Header = "Нет доступа";
                msg.UrlName = "К проектам";
                return View("Message", msg);
            }



            DocumentationModel model = new DocumentationModel();
            
            

            model.BookModel = booksCache.GetBookMode(bookId);
          
            List<Task> tasks = new List<Task>();
            if(selectedPageId!=0)
            {

               
                    model.BookModel.SetSelectedModel(selectedPageId);
                
               
               
                    DocumentPage selectedDocPage = docDB.GetPage(selectedPageId);
                    
                    selectedDocPage.WhatchCount++;
                    
                    docDB.UpdatePage(selectedDocPage);
                    
                     
                    model.SelectedPage = new SelectedPageModel();
                    model.SelectedPage .WhatchCount=selectedDocPage.WhatchCount;
                    model.SelectedPage.SetFromPage(selectedDocPage);
                    Users.User Autor = usersDb.GetUser(selectedDocPage.AutorId);
                    if (Autor != null)
                    {
                        model.SelectedPage.AutorName = Autor.GetName();
                    }


        
                PageContent[] contents= docDB.GetPageContentsForPage(selectedPageId);
                model.SelectedPage.PageContentsModel = PageContentModel.Convert(contents);
                
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
            booksCache.UpdateInCache(bookId);
            

            return RedirectToAction("Watch", new { bookId = bookId});
        }
        public IActionResult AddBasePage(int bookId)
        {
            User user = usersDb.GetUser(User.Identity.Name);

            DocumentPage nPAge = new DocumentPage();
            nPAge.AutorId = user.Id;
            nPAge.BookId = bookId;
            nPAge.ParrentId = 0;
            nPAge.Name = "BasePage";
            docDB.AddPage(nPAge);
            booksCache.UpdateInCache(bookId);
            return RedirectToAction("Watch", new { bookId = bookId, selectedPage=nPAge.Id });
        }
         
        public IActionResult AddPage(int ParrentPageId)
        {

           
            User user = usersDb.GetUser(User.Identity.Name);

            DocumentPage parrent= docDB.GetPage(ParrentPageId);
            List<DocumentPage> pages = docDB.GetPagesForbook(parrent.BookId);

            DocumentPage nPage = new DocumentPage();
            nPage.Name = $"New Page ({pages.Count+1})";
            nPage.ParrentId = parrent.Id;
            nPage.BookId = parrent.BookId;
            nPage.AutorId = user.Id;
            docDB.AddPage(nPage);

            booksCache.UpdateInCache(parrent.BookId);

            return RedirectToAction("Watch",new {bookId= parrent.BookId, selectedPage = nPage.Id});
        }

        
    }
}
