using Microsoft.AspNetCore.Mvc;
using Worky.Data.Project;
using Worky.Data.Tags;
using Worky.Models.TaskTags;
using Worky.Project.Tags;

namespace Worky.Controllers
{
    public class TaskTagsController : Controller
    {
        ITagsDb tags;
        IProjectDb projects;
        public TaskTagsController(ITagsDb db, Data.Project.ProjectDbContext prj)
        {
            tags = db;
            projects = prj;
        }
        [HttpGet]      
        public IActionResult Add(int TaskId)
        {
            Project.Task.Task task = projects.GetTaskById(TaskId);


            AddTagsToTaskModel model = new AddTagsToTaskModel();
            model.TaskId = TaskId;

             TagType[] existTagTypes = tags.GetTagTypesForProject(task.ProjectId);
            model.SetExistTagTypes(existTagTypes);

            return View(model);
        }
        [HttpPost]
        public IActionResult Add(AddTagsToTaskModel model)
        {
            Project.Task.Task task = projects.GetTaskById(model.TaskId);

            string[] tagNames = model.GetTags();

            foreach (string tagName in tagNames)
            {
                TagType[] tagsInProject = tags.GetTagTypesForProject(task.ProjectId);
                TagType existTagTypt = tagsInProject.Where(i => i.Name == tagName).FirstOrDefault();


                if(existTagTypt==null)
                {
                    existTagTypt = tags.CreateTagType(task.ProjectId, tagName);
                }
                tags.CreateTagTaskInstance(task.Id, existTagTypt.Id);

            }

            return Redirect($"~/Task/Edit?TaskId={model.TaskId}");
        }
        

    }
}
