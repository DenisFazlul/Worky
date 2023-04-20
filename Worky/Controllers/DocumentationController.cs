using Microsoft.AspNetCore.Mvc;
using Worky.Data.Project;
using Worky.Models.Project;
namespace Worky.Controllers
{
    public class DocumentationController : Controller
    {
        IDocumentCollection docDb;
        IProjectDb projectDB;
        public DocumentationController(ProjectDbContext context)
        {
            projectDB = context;
            docDb = context;
        }
        public IActionResult Pages(int ProjectId,int seletedPageId=0)
        {
             DocumentationModel model=new  DocumentationModel();
            List<Project.Documents.DocIerarhy> pagesIerarhy = projectDB.GetPagesForProject(ProjectId);
            model.SetIerarhy(pagesIerarhy);
            model.PorjecId = ProjectId;

            DocumentModel pageModel = new DocumentModel();
            model.SelectedPage = pageModel;
            if(seletedPageId!=0)
            {
                pageModel.Document = docDb.GetDocById(seletedPageId);
            }
            
          
            return View(model);
        }
        public IActionResult Add(int pid,int ParentId)
        {
             
            Worky.Project.Documents.DocIerarhy ir = new Project.Documents.DocIerarhy();
            ir.ProjectId = pid;
            ir.ParrentId = ParentId;

            Project.Documents.Document doc = new Project.Documents.Document();
            doc.Name = "New";
            docDb.AddDocument(doc);
            ir.DocId = doc.Id;
            projectDB.AddDocIerarhy(ir);
            return RedirectToAction("Pages", new { pid = pid });
        }
        public IActionResult Remove(int DocIrId,int projectId)
        {


            return RedirectToAction("Pages", new { pid = projectId });
        }
        public IActionResult ProjectPages(int pid)
        {
            return View();
        }

        [HttpGet]
        public Models.Project.DocumentationModel GetPages(int pid)
        {
            Models.Project.DocumentationModel model = new Models.Project.DocumentationModel();
            
            model.PorjecId = pid;
            model.SetIerarhy(projectDB.GetPagesForProject(pid));
            return model;
            
        }
    
    }
}
