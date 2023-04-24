using Worky.Data.Project;
using Worky.Models.DocModels;

namespace Worky.Services
{
    public interface IBookCashe
    {
        public void SetDb(IDocmentationDB db);
        public BookModel GetBookMode(int bookId);
        public BookModel Create(int bookId);
        public void UpdateInCache(int bookId);
         
    }
}
