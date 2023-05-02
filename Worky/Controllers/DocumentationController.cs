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
        IBookCashe bookCashe;
        IUsersCollection users;
        Data.IIviteCollection Invites;
        IProjectDb projectDb;
        public DocumentationController(ProjectDbContext db,IBookCashe cash)
        {
            docDB = db;
            bookCashe = cash;
            bookCashe.SetDb(db);
            users = db;
            Invites = db;
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
       
        public IActionResult Watch(int bookId,int selectedPage=0)
        {
            DocumentationBook exist= docDB.GetBookByid(bookId);
            
            Users.User curUser = users.GetUser(User.Identity.Name);
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
            Services.UserAcsessService ac = new Services.UserAcsessService(Invites, projectDb);
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
            
            

            model.BookModel = bookCashe.GetBookMode(bookId);
          
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
                    Users.User Autor = users.GetUser(selectedDocPage.AutorId);
                    if (Autor != null)
                    {
                        model.SelectedPage.AutorName = Autor.GetName();
                    }


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
            bookCashe.UpdateInCache(bookId);
            

            return RedirectToAction("Watch", new { bookId = bookId});
        }
        public IActionResult AddBasePage(int bookId)
        {
            User user = users.GetUser(User.Identity.Name);

            DocumentPage nPAge = new DocumentPage();
            nPAge.AutorId = user.Id;
            nPAge.BookId = bookId;
            nPAge.ParrentId = 0;
            nPAge.Name = "BasePage";
            docDB.AddPage(nPAge);
            bookCashe.UpdateInCache(bookId);
            return RedirectToAction("Watch", new { bookId = bookId, selectedPage=nPAge.Id });
        }
         
        public IActionResult AddPage(int ParrentPageId)
        {

           
            User user = users.GetUser(User.Identity.Name);

            DocumentPage parrent= docDB.GetPage(ParrentPageId);
            List<DocumentPage> pages = docDB.GetPagesForbook(parrent.BookId);

            DocumentPage nPage = new DocumentPage();
            nPage.Name = $"New Page ({pages.Count+1})";
            nPage.ParrentId = parrent.Id;
            nPage.BookId = parrent.BookId;
            nPage.AutorId = user.Id;
            docDB.AddPage(nPage);

            bookCashe.UpdateInCache(parrent.BookId);

            return RedirectToAction("Watch",new {bookId= parrent.BookId, selectedPage = nPage.Id});
        }

        
    }
}
