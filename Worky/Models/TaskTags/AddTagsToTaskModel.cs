using Worky.Project.Tags;

namespace Worky.Models.TaskTags
{
    public class AddTagsToTaskModel
    {
        public int TaskId { get; set; }
        public string InputTags { get; set; }
        public List<string> ExistTags { get; set; }
        public AddTagsToTaskModel()
        {
            this.ExistTags=new List<string>();
        }
        public string[] GetTags()
        {
            string [] tags = InputTags.Split(',');

            List<string> taggNames= new List<string>();
            foreach(string tag in tags)
            {
                if(string.IsNullOrEmpty(tag)==false)
                {
                    taggNames.Add(tag);
                }
            }
            return taggNames.ToArray(); 
        }

        internal void SetExistTagTypes(TagType[] existTagTypes)
        {
            foreach(TagType type in existTagTypes)
            {
               this.ExistTags.Add(type.Name);
            }
        }
    }
}
