using Microsoft.EntityFrameworkCore;
using Worky.Project.Tags;

namespace Worky.Data.Tags
{
    public class TagsDb: DbContext,ITagsDb
    {
        public DbSet<TagType> TagTypes { get; set; }
        public DbSet<TagTaskInstance> TagTaskInstances { get; set; }
        public TagsDb(DbContextOptions<TagsDb> options)
        : base(options)
        {
            Database.EnsureCreated();


        }

        public TagType CreateTagType(int ProjectId, string Name)
        {
            TagType t=new TagType() { ProjectId = ProjectId, Name = Name };
            TagTypes.Add(t);
            this.SaveChanges();
            return t;
        }

        public TagType[] GetTagTypesForProject(int ProjectId)
        {
            return this.TagTypes.Where(i=>i.ProjectId== ProjectId).ToArray();
        }

        public TagTaskInstance? CreateTagTaskInstance(int TaskId, int TagTypeId)
        {
           
            TagTaskInstance tagTaskInstance = TagTaskInstances.Where(i=>i.TaskId== TaskId).Where(i=>i.TagTypeId==TagTypeId).FirstOrDefault();
            if(tagTaskInstance!=null)
            {
                return tagTaskInstance;
            }

            TagType existTagtype = TagTypes.Where(i => i.Id == TagTypeId).FirstOrDefault();
            if(existTagtype!=null)
            {
                tagTaskInstance = new TagTaskInstance();
                tagTaskInstance.TaskId = TaskId;
                tagTaskInstance.TagTypeId=TagTypeId;
                this.TagTaskInstances.Add(tagTaskInstance);
                this.SaveChanges();

            }
            return tagTaskInstance;
        }

        public void RemoveTagTaskInstance(int TagTaskInstanceId)
        {
           TagTaskInstance exist=this.TagTaskInstances.Where(i=>i.Id==TagTaskInstanceId).FirstOrDefault();
            if(exist!=null)
            {

               
                this.TagTaskInstances.Remove(exist);
            }
            this.SaveChanges();

            List<TagTaskInstance> similarTags = this.TagTaskInstances.Where(i => i.TagTypeId == exist.TagTypeId).ToList();
            if(similarTags.Count==0)
            {
                TagType type=this.TagTypes.Where(i=>i.Id== exist.TagTypeId).FirstOrDefault();
                this.TagTypes.Remove(type);
                this.SaveChanges();
            }
            
        }

        public TagTaskInstance[] GetTagTaskInstancesForTask(int TaskId)
        {
            return this.TagTaskInstances.Where(i=>i.TaskId==TaskId).ToArray();
        }

        public TagType GetTagTypeByid(int tagTypeId)
        {
            return TagTypes.Where(i=>i.Id == tagTypeId).FirstOrDefault();
        }

        public TagTaskInstance GetTagInstanceById(int tagInstanceId)
        {
           return TagTaskInstances.Where(i=>i.Id == tagInstanceId).FirstOrDefault();
        }
    }
}
