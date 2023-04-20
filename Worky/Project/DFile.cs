namespace Worky.Project
{
    public class DFile
    {
        public DFile()
        {

        }
        public DFile(IFormFile uploadedFile)
        {
            using (var ms = new MemoryStream())
            {
                uploadedFile.CopyTo(ms);
                this.Bytes = ms.ToArray();
                
            }
            this.ContentType = uploadedFile.ContentType;
            this.Name = uploadedFile.FileName;
        }

        public int Id { get; set; }
        public byte[] Bytes { get; set; }
        public string ContentType { get; set; }
        public string Name { get; set; }

        internal void AddToDb()
        {
            Data.Project.IdFilesDb col = Data.DB.GetFileDb();
            col.AddFile(this);
        }

        internal void Delete()
        {
            Data.Project.IdFilesDb files = Data.DB.GetFileDb();
            files.Remove(this);
        }
    }
}
