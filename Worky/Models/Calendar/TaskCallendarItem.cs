namespace Worky.Models.Calendar
{
    public class TaskCallendarItem : ICallendarEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime End { get; set; }
        public DateTime Start { get; set; }

        public string GetEditLink()
        {
            return $"Task/Edit?TaskId={Id}";
        }
    }
}
