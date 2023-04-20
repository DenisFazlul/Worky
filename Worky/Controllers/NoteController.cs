using Microsoft.AspNetCore.Mvc;
using Worky.Data.Project;
namespace Worky.Controllers
{
    public class NoteController : Controller
    {
        IProjectDb projectDb;
        public NoteController(ProjectDbContext db)
        {
            projectDb= db;
        }
        [HttpGet]
        public IActionResult EditNote(int NoteId)
        {
           
            Project.Note note = projectDb.GetNote(NoteId);
            Models.Project.NoteEditModel model = new Models.Project.NoteEditModel(note);
         
            return View(model);
        }
        [HttpPost]
        public IActionResult EditNote(Models.Project.NoteEditModel model)
        {

             
            Project.Note note = projectDb.GetNote(model.Id);
            note.SetData(model);
            projectDb.Update(note);
            return RedirectToAction("ProjectNotes", "Notes", new { ProjectId = model.ProjectId });
        }
    }
}
