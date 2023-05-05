namespace Worky.Models.Calendar
{
    public class DetailDayEventsModel
    {
        public bool IsValid { get; set; } = true;
        public Day Day { get; set; }
        public List<ICallendarEvent> DailyEvents { get; set; }
        
    }
}
