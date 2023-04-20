using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Worky.Data.Project;
using Worky.Models.Project;
using Worky.Project;

namespace Worky.Controllers
{
    public class FilesController : Controller
    {
        Data.Project.IdFilesDb col;
        Data.Project.IProjectDb prj;
        public FilesController(ProjectDbContext context)
        {
            col = context;
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
                    col.AddFile(file);
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
            Data.Project.IProjectDb prj = Data.DB.GetProject();
            Data.Project.IdFilesDb files = Data.DB.GetFileDb();
            TaskFile f = prj.GetTaskFileById(TaskFileId);
            DFile dFile = files.GetById(f.FileId);
            return DownloadFile(dFile.Id);
        }
        
        public IActionResult  DownloadFile(int id)
        {
            Data.Project.IdFilesDb col = Data.DB.GetFileDb();
            DFile file = col.GetById(id);

           
            return File(file.Bytes, file.ContentType);
        }
        [HttpPost]
        public void RemoveTaskFile(int TaskFileId)
        {
            Data.Project.IProjectDb prj = Data.DB.GetProject();
            Data.Project.IdFilesDb files = Data.DB.GetFileDb();
            TaskFile f = prj.GetTaskFileById(TaskFileId);
            DFile dFile = files.GetById(f.FileId);
            prj.RemoveTaskFile(f);
            files.Remove(dFile);

        }


    }
}
