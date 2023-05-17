using Microsoft.AspNetCore.Mvc;
using Worky.Data.Project;
using Worky.Models.DocModels;
using Worky.Project.Documents;

namespace Worky.Controllers
{
    public class PageContentEditController : Controller
    {
        IDocmentationDB docDB;
        IPageContentType PageContentTypeDB;
        public PageContentEditController(ProjectDbContext db)
        {
            docDB = db;
            PageContentTypeDB = db;
        }
        [HttpGet] 
        public IActionResult EditPageContent(int PageId)
        {
            DocumentPage page= docDB.GetPage(PageId); 
            PageContent[] contents=docDB.GetPageContentsForPage(PageId);
            EditPageModel model = new EditPageModel();
            model.Id = PageId;
            model.Content = ContentsModels(contents);


            return View(model);
        }
        public List<PageContentModel> ContentsModels(PageContent[] c)
        {
            List<PageContentModel> contents= new List<PageContentModel>();
            return contents;
        }
        
        
        [HttpGet]
        public IActionResult SelectTemplate(int PageId)
        {


            return View();
        }
        [HttpGet]
        public IActionResult AddPageContent(int typeId,int PageId)
        {
            return View();
        }
    }
}
