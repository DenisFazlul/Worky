using Worky.Models.DocModels;
namespace Worky.Project.Documents
{
    public class PagesModelsSorter
    {
        
        List<PageModel> _pagesModel;
        public PageModel BasePage;
         
        public PagesModelsSorter()
        {
            _pagesModel = new List<PageModel>();
        }
        public void Sort(List<PageModel> pages)
        {

            _pagesModel = pages;
            
            RunSort();
        }
        private void RunSort()
        {
            foreach(PageModel model in _pagesModel)
            {
                if (model.ParrentId != 0)
                {
                    PageModel parrent = GetBuId(model.ParrentId);
                    parrent.Pages.Add(model);
                }
                if(model.ParrentId==0)
                {
                    BasePage = model;
                }
                
            }
        }
         
         
        public PageModel GetBuId(int id)
        {
            
            return _pagesModel.Where(i => i.Id == id).FirstOrDefault();
        }
        public static PageModel createPageModel(DocumentPage d)
        {
            PageModel model = new PageModel();
            model.Id = d.Id;
            model.bookId = d.BookId;
            model.ParrentId = d.ParrentId;
            model.Name = d.Name;
            return model;
        }

         
    }
}
