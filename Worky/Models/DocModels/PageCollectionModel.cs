using System.Collections;

namespace Worky.Models.DocModels
{
    public class PageCollectionModel 
    {
        public List<PageModel> _pages { get; set; }
        
        public PageCollectionModel()
        {
            _pages = new List<PageModel>();
        }
       public void Add(PageModel p)
        {
            _pages.Add( p);
        }
        public void Remove(PageModel p)
        {
            PageModel exist = _pages.Where(i => i.Id == p.Id).FirstOrDefault();
            _pages.Remove(exist);
        }
        public PageModel GetPage(int id)
        {
            PageModel exist = _pages.Where(i => i.Id == id).FirstOrDefault();
            return exist;
        }

      
        internal IEnumerable<PageModel> ToList()
        {
            return this._pages;
        }
    }
}
