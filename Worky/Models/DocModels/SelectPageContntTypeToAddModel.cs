namespace Worky.Models.DocModels
{
    public class SelectPageContntTypeToAddModel
    {
        public List<ContentTypeModel> ContentType { get; set; }
        public int PageId { get; set; }
        public SelectPageContntTypeToAddModel()
        {
            this.ContentType = new List<ContentTypeModel>();
        }
        public ContentTypeModel GetSelected()
        {
            return this.ContentType.Where(i => i.IsCheked).FirstOrDefault();
        }

        
    }
}
