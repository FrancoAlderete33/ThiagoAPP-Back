using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStack.API.Models
{
    public class Sleep
    {
        public int Id { get; set; }
        public DateTime start_time { get; set; } // Hora de inicio de sueño
        public DateTime end_time { get; set; } // Hora de fin de sueño

        [Column(TypeName = "decimal(18,2)")]
        public Decimal durationInMinutes { get; set; } // Duracion total de sueño
        public DateTime date { get; set; } // Fecha del periodo de  sueño
    }
}
