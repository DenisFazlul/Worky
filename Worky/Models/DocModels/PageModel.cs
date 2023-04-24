namespace Worky.Models.DocModels
{
    public class PageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int bookId { get; set; }
        public int ParrentId { get; set; }
        public bool IsSelected { get; set; } = false;
        
        public PageCollectionModel Pages { get; set; }
        public PageModel()
        {
            Pages = new PageCollectionModel();
        }
        public void Set(Worky.Project.Documents.DocumentPage page)
        {
            this.Id=page.Id;
            this.Name=page.Name;
        }
        public override string ToString()
        {
            return $"{Id}-{Name}";
        }

        internal List<int> GetChildsIds()
        {
            List<int> result = new List<int>();  
             foreach(PageModel page in this.Pages.ToList())
            {
                result.Add(page.Id);
                List<int> childIds = GetChildsRecocive(page);
                result.AddRange(childIds);
            }
            return result;
        }
        private List<int> GetChildsRecocive(PageModel parrent)
        {
            List<int> ids = new List<int>();
            foreach (PageModel child in parrent.Pages.ToList())
            {
                ids.Add(child.Id);
                List<int> childIds = GetChildsRecocive(child);
                ids.AddRange(childIds);
            }
            return ids;
        }
    }
}
