using System.ComponentModel.DataAnnotations.Schema;

namespace Worky.Project.Documents
{
    public class DocumentPage
    {
        public int Id { get; set; }
        public string Name { get; set; } = "NewPage";
         
        public int ParrentId { get; set; }
        public int BookId { get; set; }
        public string Description { get; set; } = "Short description";
        public int AutorId { get; set; }
        public int WhatchCount { get; set; }

    }
}
