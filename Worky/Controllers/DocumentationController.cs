using Microsoft.AspNetCore.Mvc;

namespace Worky.Controllers
{
    public class DocumentationController : Controller
    {
        public IActionResult Pages(int pid)
        {
            Models.Project.DocumentationModel model=new Models.Project.DocumentationModel();
            Data.Project.IProjectDb col = Data.DB.GetProject();
            model.PorjecId = pid;
            model.SetIerarhy(col.GetPagesForProject(pid));
            return View(model);
        }
        public IActionResult Add(int pid,int ParentId)
        {
            Data.Project.IProjectDb col = Data.DB.GetProject();
            Worky.Project.Documents.DocIerarhy ir = new Project.Documents.DocIerarhy();
            ir.ProjectId = pid;
            ir.ParrentId = ParentId;

            Project.Documents.Document doc = new Project.Documents.Document();
            doc.Name = "New";
            col.AddDocument(doc);
            ir.DocId = doc.Id;
            col.AddDocIerarhy(ir);
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
            Data.Project.IProjectDb col = Data.DB.GetProject();
            model.PorjecId = pid;
            model.SetIerarhy(col.GetPagesForProject(pid));
            return model;
            
        }
    
    }
}
