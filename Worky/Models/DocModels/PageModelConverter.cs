using Worky.Project.Documents;

namespace Worky.Models.DocModels
{
    public class PageModelConverter
    {
        public static List<PageModel> Convert(List<DocumentPage> docs)
        {
            List<PageModel> result = new List<PageModel>(); 
            foreach (DocumentPage p in docs)
            {

                PageModel model = createPageModel(p);
                result.Add(model);

            }
            return result;
        }
        public static PageModel createPageModel(DocumentPage d)
        {
            PageModel model = new PageModel();
            model.Id = d.Id;
            model.bookId = d.BookId;
            model.ParrentId = d.ParrentId;
            model.Name = d.Name;
            model.AutorId = d.AutorId;
            return model;
        }
    }
}
