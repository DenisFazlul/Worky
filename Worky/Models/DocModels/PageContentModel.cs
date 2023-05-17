using Worky.Project.Documents;

namespace Worky.Models.DocModels
{
    public class PageContentModel
    {
        public int id { get; set; }
        public int PageId { get; set; }
        public string Content { get; set; } = "";
        public string Head { get; set; } = "";
        public ContentType ContentType { get;   set; }

        public static PageContentModel[] Convert(PageContent[] pages)
        {
            PageContentModel[] models = new PageContentModel[pages.Length];
            for (int i = 0; i < pages.Length; i++)
            {
                PageContentModel model = new PageContentModel();
                model.Set(pages[i]);
                models[i] = model;
            }
            return models;
        }

        private void Set(PageContent pageContent)
        {
            this.id = pageContent.Id;
            this.Content = pageContent.Content;
            this.ContentType = pageContent.ContentType;
            this.PageId = pageContent.PageId;
            this.Head = pageContent.Head;
        }

        internal string GetContent()
        {
            if(string.IsNullOrEmpty(Content))
            {
                return "";
            }
            return Content;
        }

        internal string GetHead()
        {
            if (string.IsNullOrEmpty(Head))
            {
                return "";
            }
            return Head;
        }
    }
}
