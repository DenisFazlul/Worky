using Microsoft.AspNetCore.Mvc;
using Worky.Data.Project;
using Worky.Models.DocModels;
using Worky.Project.Documents;
using Worky.Services;

namespace Worky.Controllers
{
    public class DocEditController : Controller
    {
        IDocmentationDB docDB;
        IBookCashe bookCashe;
        public DocEditController(ProjectDbContext db, IBookCashe cash)
        {
            docDB = db;
            bookCashe = cash;
            bookCashe.SetDb(db);
        }
        public IActionResult Index(int PageId)
        {
            EditPageModel model = new EditPageModel();
            
            DocumentPage page= docDB.GetPage(PageId);
            model.Description = page.Description;
            model.bookId = page.BookId;
            model.Id=page.Id;
            model.Name=page.Name;
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(EditPageModel model)
        {
            DocumentPage page = docDB.GetPage(model.Id);
            page.Name = model.Name;
            page.Description = model.Description;
            docDB.UpdatePage(page);

            BookModel m= bookCashe.GetBookMode(model.bookId);
           PageModel pg= m.AllPages.Where(i => i.Id == page.Id).FirstOrDefault();
            pg.Name=model.Name;
            
            return RedirectToAction("Index",new {PageId= model.Id }); 
        }
    }
}
