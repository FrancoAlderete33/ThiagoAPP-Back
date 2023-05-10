namespace FullStack.API.Models
{
    public class Calendar
    {
        public int Id { get; set; }
        public string title { get; set; } // Título del evento
        public string description { get; set; } // Descripción del evento
        public DateTime timeEventStart { get; set; } // Hora  del evento
        public DateTime date { get; set; } // Fecha del evento
    }
}
