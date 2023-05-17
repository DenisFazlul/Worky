
using Worky.Project.Documents;

namespace Worky.Models.DocModels
{
    public class ContentTypeModel
    {
        public ContentType type { get; set; }
        public string DisplayName = "";
        public bool IsCheked { get; set; } = false;
        public static List<ContentTypeModel> getmodels()
        {
            List<ContentTypeModel> models = new List<ContentTypeModel>()
            {
                new ContentTypeModel(){type=ContentType.header,DisplayName="Заголовок"},
                new ContentTypeModel(){type=ContentType.mainText,DisplayName="Текст"},
                new ContentTypeModel(){type=ContentType.code,DisplayName="Код"},
                new ContentTypeModel(){type =ContentType.link,DisplayName="Ссылка" },
            };




            return models;
        }
    }
}
