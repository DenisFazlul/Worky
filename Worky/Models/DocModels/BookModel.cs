using Newtonsoft.Json;
using Worky.Project.Documents;

namespace Worky.Models.DocModels
{
    public class Root
    {
        public BookModel Book { get; set; }
    }
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public PageModel StartPage { get; set; }
        public BookModel()
        {
            
        }
     
         
         

        
        
    }
}
