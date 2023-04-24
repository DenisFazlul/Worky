using Microsoft.AspNetCore.Mvc;
using Worky.Data.Project;
using Worky.Models.DocModels;
using Worky.Project.Documents;

namespace Worky.Controllers
{
    public class DocEditController : Controller
    {
        IDocmentationDB docDB;

        public DocEditController(ProjectDbContext db)
        {
            docDB = db;
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

            return RedirectToAction("Index",new {PageId= model.Id }); 
        }
    }
}
