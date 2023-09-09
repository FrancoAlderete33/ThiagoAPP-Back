using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStack.API.Models
{
    public class Breastfeeding
    {
        public int Id { get; set; } 
        public DateTime start_time { get; set; } 
        public DateTime end_time { get; set; } 

        [Column(TypeName = "decimal(18,2)")]
        public Decimal durationInMinutes { get; set; } 
        public DateTime date { get; set; } 
    }


}
