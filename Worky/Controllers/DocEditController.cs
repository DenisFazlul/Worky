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
        IPageContentType PageContentTypeDB;
        public DocEditController(ProjectDbContext db, IBookCashe cash)
        {
            docDB = db;
            bookCashe = cash;
            bookCashe.SetDb(db);
            PageContentTypeDB = db;
        }
        public IActionResult Index(int PageId)
        {
            EditPageModel model = new EditPageModel();
            
            DocumentPage page= docDB.GetPage(PageId);
            model.Description = page.Description;
            model.bookId = page.BookId;
            model.Id=page.Id;
            model.Name=page.Name;

            PageContent[] contents = docDB.GetPageContentsForPage(PageId);
            model.SetContent(contents);

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
        [HttpGet]
        public IActionResult SelectPageContentTypeToAdd(int PageId)
        {
            SelectPageContntTypeToAddModel model = new SelectPageContntTypeToAddModel();
            model.PageId= PageId;
            model.ContentType=ContentTypeModel.getmodels();
            
            return View(model);
        }
        [HttpPost]
        public IActionResult SelectPageContentTypeToAdd(SelectPageContntTypeToAddModel model)
        {
            ContentTypeModel sel = model.GetSelected();
             
            
           PageContent   content= docDB.AddPageContent(model.PageId);
            content.ContentType = sel.type;
            docDB.UpdatePageContent(content);


            return RedirectToAction("Index", new { PageId = model.PageId });
        }

        [HttpGet]
        public IActionResult EditPageContent(int PageContentId)
        {
            PageContent content = docDB.GetPageContentById(PageContentId);
            PageContentModel model = PageContentModel.Convert(new PageContent[] { content }).FirstOrDefault();

            return View(model);
        }
        [HttpPost]
        public IActionResult EditPageContent(PageContentModel model)
        {
            PageContent content= docDB.GetPageContentById(model.id);
            content.Content = model.GetContent() ;
            content.Head = model.GetHead();
            docDB.UpdatePageContent(content);
            return RedirectToAction("Index", new { PageId = model.PageId });
        }

    }
}
