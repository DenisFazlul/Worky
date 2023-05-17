using Worky.Project.Documents;

namespace Worky.Models.DocModels
{
    public class EditPageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int bookId { get; set; }
        public List<PageContentModel> Content { get; set; }
        public EditPageModel()
        {
            Content = new List<PageContentModel>();
        }

        internal void SetContent(PageContent[] contents)
        {
             foreach(PageContent content in contents)
            {
                PageContentModel m=new PageContentModel();
                m.id = content.Id;
                m.Content = content.Content;
                m.Head = content.Head;
                m.ContentType = content.ContentType;
                Content.Add(m);
                   
            }
        }
    }
}
