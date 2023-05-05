using Microsoft.AspNetCore.Mvc;
using Worky.Data.Project;
using Worky.Data.Tags;
using Worky.Models.TaskTags;
using Worky.Project.Tags;

namespace Worky.Controllers
{
    public class TaskTagsController : Controller
    {
        ITagsDb Tags;
        IProjectDb Projects;
        public TaskTagsController(ITagsDb tagDb, Data.Project.ProjectDbContext prj)
        {
            Tags = tagDb;
            Projects = prj;
        }
        [HttpGet]      
        public IActionResult Add(int TaskId)
        {
            Project.Task.Task task = Projects.GetTaskById(TaskId);


            AddTagsToTaskModel model = new AddTagsToTaskModel();
            model.TaskId = TaskId;

             TagType[] existTagTypes = Tags.GetTagTypesForProject(task.ProjectId);
            model.SetExistTagTypes(existTagTypes);

            return View(model);
        }
        [HttpPost]
        public IActionResult Add(AddTagsToTaskModel model)
        {
            Project.Task.Task task = Projects.GetTaskById(model.TaskId);

            string[] tagNames = model.GetTags();

            foreach (string tagName in tagNames)
            {
                TagType[] tagsInProject = Tags.GetTagTypesForProject(task.ProjectId);
                TagType existTagTypeInProject = tagsInProject.Where(i => i.Name == tagName).FirstOrDefault();
                if(existTagTypeInProject==null)
                {
                    existTagTypeInProject = Tags.CreateTagType(task.ProjectId, tagName);
                }
                Tags.CreateTagTaskInstance(task.Id, existTagTypeInProject.Id);

            }

            return Redirect($"~/Task/Edit?TaskId={model.TaskId}");
        }

        [HttpGet]
        public IActionResult RemoveTag(int tagInstanceId)
        {
            int TaskId = 0;
            TagTaskInstance inst= Tags.GetTagInstanceById(tagInstanceId);
           

            TaskId= inst.TaskId;

            Tags.RemoveTagTaskInstance(tagInstanceId);
            return Redirect($"~/Task/Edit?TaskId={TaskId}");
        }
        

    }
}
