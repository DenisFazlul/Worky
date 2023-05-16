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

        

        
    }
}
