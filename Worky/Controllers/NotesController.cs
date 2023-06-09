﻿using Microsoft.AspNetCore.Mvc;

namespace Worky.Controllers
{
    public class NotesController : Controller
    {
        Data.Project.IProjectCollection Projects;
        Users.IUsersCollection Users;
        public NotesController(Data.Project.ProjectDbContext projects)
        {
            Projects = projects;
        }
        public IActionResult ProjectNotes(int ProjectId)
        {
            

            Project.Project project = Projects.GetProject(ProjectId);
            
           
                
            project.GetNotes();
            Models.Project.NotesModel model = new Models.Project.NotesModel(project);
            model.Notes = project.Notes;
           
           
            return View(model);
        }
        public IActionResult AddNote(int ProjectId)
        {
            Project.Project project = Projects.GetProject(ProjectId);

            Project.Note note = new Project.Note();
            project.AddNote(note);
            
            return RedirectToAction("ProjectNotes",new {ProjectId=ProjectId});
        }
        public IActionResult DeleteNote(int ProjectId,int NotId)
        {
            this.Projects.RemoveNote(NotId);

            return RedirectToAction("ProjectNotes", new { ProjectId = ProjectId });
        }
        
    }
}
