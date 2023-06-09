﻿namespace Worky.Project
{
    public class Note
    {
        public int Id { get; set; }
      
        public int ProjectId { get; set; }
        public string Name { get; set; } = "NewNote";
        public string Description { get; set; } = "NoteDescription";
        public void SetData(Note note)
        {
            this.Id = note.Id;
            this.ProjectId=note.ProjectId ;
            this.Name = note.Name;
            this.Description = note.Description;
        }
        public void SetData(Models.Project.NoteEditModel model)
        {
            this.Id = model.Id;
            this.ProjectId = model.ProjectId;
            this.Name = model.Name;
            this.Description = model.Description;

        }

    }
}
