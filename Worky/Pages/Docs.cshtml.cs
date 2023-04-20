using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Worky.Pages
{
    public class DocsModel : PageModel
    {
        public int ProjectId;
        public Models.Project.DocumentationModel model;
        public void OnGet(int pid)
        {
            this.ProjectId = pid;

             model = new Models.Project.DocumentationModel();
            Data.Project.IProjectDb col = Data.DB.GetProject();
            model.PorjecId = pid;
            model.SetIerarhy(col.GetPagesForProject(pid));
        }
    }
}
