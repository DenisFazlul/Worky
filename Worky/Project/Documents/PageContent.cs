namespace Worky.Project.Documents
{
    public class PageContent
    {
        public int Id { get; set; }
        public int PageId { get; set; }
         

        public int SortIndex { get; set; }
        public string Content { get; set; }
        public int Type { get; set; }
        public void Set(PageContent page)
        {
            this.Id = page.Id;
            this.PageId = page.PageId;
            this.SortIndex = page.SortIndex;
            this.Content = page.Content;
            this.Type = page.Type;

        }

    }
}
