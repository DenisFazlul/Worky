using Worky.Project;

namespace Worky.Models.Project
{
    public class NoteEditModel
    {
     
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public NoteEditModel(Note note)
        {
            this.Id = note.Id;
            this.ProjectId = note.ProjectId;
            this.Name = note.Name;
            this.Description = note.Description;

        }
        public NoteEditModel()
        {

        }

    }
}
