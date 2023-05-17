namespace Worky.Project.Documents
{
    public enum ContentType
    {
        header,
        mainText,
        code,
        image,
        file,
        link,

    }
    
    public class PageContent
    {
        public int Id { get; set; }
        public int PageId { get; set; }
         

        public int SortIndex { get; set; }
        public string Content { get; set; } = "";

        public ContentType ContentType { get; set; }
        public string Head { get; set; } = "";
        
        public void Set(PageContent page)
        {
            this.Id = page.Id;
            this.PageId = page.PageId;
            this.SortIndex = page.SortIndex;
            this.Content = page.Content;
            this.Head=page.Head;
            this.ContentType = page.ContentType;

        }

    }
}
