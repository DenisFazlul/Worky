namespace Worky.Models.Project
{
    public class NotesModel
    {
        public int ProjectId { get; set; }
        public List<Worky.Project.Note> Notes { get; set; }
        public NotesModel()
        {
            Notes=new List<Worky.Project.Note>();
        }
        public NotesModel(Worky.Project.Project p)
        {
            this.ProjectId = p.Id;
            this.Notes = p.Notes;
            Notes = p.Notes;
        }
        
    }
}
