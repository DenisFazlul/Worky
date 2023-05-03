using Worky.Project.Tags;
namespace Worky.Data.Tags
{
    public interface ITagsDb
    {
        public TagType CreateTagType(int ProjectId, string Name);
        public TagType GetTagTypeByid(int tagTypeId);
        public TagTaskInstance GetTagInstanceById(int tagInstanceId);
            
        public TagType[] GetTagTypesForProject(int ProjectId);
        public TagTaskInstance CreateTagTaskInstance(int TaskId, int TagTypeId);
        public void RemoveTagTaskInstance(int TagTaskInstanceId);
       
        public TagTaskInstance[] GetTagTaskInstancesForTask(int TaskId);
         

    }
}
