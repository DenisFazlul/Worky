using Worky.Project.Documents;

namespace Worky.Models.Project
{
    public class DocumentationModel
    {
        public int PorjecId { get; set; }
        public List<Worky.Project.Documents.DocIerarhy> docIerarhies { get; set; }
        public DocumentModel SelectedPage { get; set; }
        public DocumentationModel()
        {
            this.docIerarhies = new List<Worky.Project.Documents.DocIerarhy>();
        }
        public void SetIerarhy(List<DocIerarhy> docIerarhies)
        {
            
            this.docIerarhies = docIerarhies.Where(i => i.ParrentId == -1).ToList();
            foreach(DocIerarhy ir in this.docIerarhies)
            {
                GetIerarhyRecurcy(docIerarhies,ir);
            }
        }

        private void GetIerarhyRecurcy(List<DocIerarhy> docIerarhies,DocIerarhy ir)
        {
            ir.docIerarhies = docIerarhies.Where(i => i.ParrentId == ir.Id).ToList();
            foreach(DocIerarhy i in ir.docIerarhies)
            {
                GetIerarhyRecurcy(docIerarhies, i);
            }

        }
    }
}
