namespace FullStack.API.Models
{
    public class Calendar
    {
        public int Id { get; set; }
        public string title { get; set; } 
        public string description { get; set; } 
        public DateTime timeEventStart { get; set; }
        public DateTime date { get; set; } 
    }
}
