using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStack.API.Models
{
    public class Breastfeeding
    {
        public int Id { get; set; } // Clave primaria
        public DateTime start_time { get; set; } // Hora de inicio de la lactancia
        public DateTime end_time { get; set; } // Hora de finalización de la lactancia

        [Column(TypeName = "decimal(18,2)")]
        public Decimal durationInMinutes { get; set; } // Duración de la lactancia
        public DateTime date { get; set; } // Fecha de la lactancia
    }


}
