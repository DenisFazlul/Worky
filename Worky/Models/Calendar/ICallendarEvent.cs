namespace Worky.Models.Calendar
{
    public interface ICallendarEvent
    {
        public string Name { get; set; }
        public DateTime End { get; set; }
        public DateTime Start { get; set; }
    }
}