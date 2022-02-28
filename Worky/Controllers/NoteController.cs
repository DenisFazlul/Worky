using Microsoft.AspNetCore.Mvc;

namespace Worky.Controllers
{
    public class NoteController : Controller
    {
        [HttpGet]
        public IActionResult EditNote(int NoteId)
        {
            Data.Project.IProjectCollection col = new Data.Project.ProjectDbContext();
            Project.Note note = col.GetNote(NoteId);
            Models.Project.NoteEditModel model = new Models.Project.NoteEditModel(note);
         
            return View(model);
        }
        [HttpPost]
        public IActionResult EditNote(Models.Project.NoteEditModel model)
        {

            Data.Project.IProjectCollection col = new Data.Project.ProjectDbContext();
            Project.Note note = col.GetNote(model.Id);
            note.SetData(model);
            col.Update(note);
            return RedirectToAction("ProjectNotes", "Notes", new { ProjectId = model.ProjectId });
        }
    }
}
