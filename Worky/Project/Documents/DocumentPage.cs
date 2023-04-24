using System.ComponentModel.DataAnnotations.Schema;

namespace Worky.Project.Documents
{
    public class DocumentPage
    {
        public int Id { get; set; }
        public string Name { get; set; } = "NewPage";
         
        public int ParrentId { get; set; }
        public int BookId { get; set; }

    }
}
