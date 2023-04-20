using System.ComponentModel.DataAnnotations.Schema;

namespace Worky.Project.Documents
{
    public class DocIerarhy
    {
        public int Id { get; set; }
        public int ParrentId { get; set; }
        public int ProjectId { get; set; }

        public int DocId { get; set; }
        [NotMapped]
        public List<DocIerarhy> docIerarhies { get; set; }
        [NotMapped]
        public string DocName { get; internal set; }
    }
}
