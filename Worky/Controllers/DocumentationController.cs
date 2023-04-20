using Microsoft.AspNetCore.Mvc;
using Worky.Data.Project;

namespace Worky.Controllers
{
    public class DocumentationController : Controller
    {
        IProjectDb projectDB;
        public DocumentationController(ProjectDbContext context)
        {
            projectDB = context;
        }
        public IActionResult Pages(int pid)
        {
            Models.Project.DocumentationModel model=new Models.Project.DocumentationModel();
             
            model.PorjecId = pid;
            model.SetIerarhy(projectDB.GetPagesForProject(pid));
            return View(model);
        }
        public IActionResult Add(int pid,int ParentId)
        {
             
            Worky.Project.Documents.DocIerarhy ir = new Project.Documents.DocIerarhy();
            ir.ProjectId = pid;
            ir.ParrentId = ParentId;

            Project.Documents.Document doc = new Project.Documents.Document();
            doc.Name = "New";
            projectDB.AddDocument(doc);
            ir.DocId = doc.Id;
            projectDB.AddDocIerarhy(ir);
            return RedirectToAction("Pages", new { pid = pid });
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
