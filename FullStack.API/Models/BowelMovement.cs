namespace FullStack.API.Models
{
    public class BowelMovement
    {
        public int Id { get; set; }
        public DateTime time { get; set; } // Hora de la defecación
        public string type { get; set; } // Tipo de la defecación, líquido/sólido
        public DateTime date { get; set; } // Fecha de la defecación
    }
}
