using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Worky.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocController : ControllerBase
    {
        [HttpGet]
        public Models.Project.DocumentationModel Get(int pid)
        {
            Models.Project.DocumentationModel model = new Models.Project.DocumentationModel();
            Data.Project.IProjectDb col = Data.DB.GetProject();
            model.PorjecId = pid;
            model.SetIerarhy(col.GetPagesForProject(pid));
            return model;
        }
    }
}
