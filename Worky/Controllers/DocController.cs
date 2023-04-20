using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Worky.Data.Project;

namespace Worky.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocController : ControllerBase
    {
        IProjectDb projectDb;
        public DocController(ProjectDbContext context)
        {
            projectDb = context;
        }
        [HttpGet]
        public Models.Project.DocumentationModel Get(int pid)
        {
            Models.Project.DocumentationModel model = new Models.Project.DocumentationModel();
            
            model.PorjecId = pid;
            model.SetIerarhy(projectDb.GetPagesForProject(pid));
            return model;
        }
    }
}
