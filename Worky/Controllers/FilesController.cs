using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Worky.Data.Project;
using Worky.Models.Project;
using Worky.Project;

namespace Worky.Controllers
{
    public class FilesController : Controller
    {
        Data.Project.IdFilesDb filesDB;
        Data.Project.IProjectDb prj;
        public FilesController(ProjectDbContext context)
        {
            filesDB = context;
            prj= context;
        }
        [HttpGet]
        public IActionResult AddTaskFile(int TaskId)
        {
            Models.AddTaskFileModel model = new Models.AddTaskFileModel()
            {
                TaskId = TaskId
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult AddTaskFile(IFormFile uploadedFile, int TaskId)
        {
            //   int TaskId = TaskId;


            
            if (uploadedFile.Length > 0)
            {

                
                    DFile file = new DFile(uploadedFile);
                    filesDB.AddFile(file);
                    TaskFile ts =new TaskFile();
                    ts.TaskId = TaskId;
                    ts.FileId = file.Id;
                    ts.Name = file.Name;
                    prj.AddTaskFile(ts);

            }
           
            return RedirectToAction("Edit","Task", new { TaskId = TaskId });
            



        }
        [HttpPost]
        public IActionResult DownloadTaskFile(int TaskFileId)
        {
            
            
            TaskFile f = prj.GetTaskFileById(TaskFileId);
            DFile dFile = filesDB.GetById(f.FileId);
            return DownloadFile(dFile.Id);
        }
        
        public IActionResult  DownloadFile(int id)
        {
            
            DFile file = filesDB.GetById(id);

           
            return File(file.Bytes, file.ContentType);
        }
        [HttpPost]
        public void RemoveTaskFile(int TaskFileId)
        {
          
            TaskFile f = prj.GetTaskFileById(TaskFileId);
            DFile dFile = filesDB.GetById(f.FileId);
            prj.RemoveTaskFile(f);
            filesDB.Remove(dFile);

        }


    }
}
