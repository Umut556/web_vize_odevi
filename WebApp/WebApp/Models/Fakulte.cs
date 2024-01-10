using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Interfaces;

namespace WebApp.Models
{
    [NotMapped]
   // [Keyless]
    public class Fakulte : IEntity
    {
        [Key]
        public int FKKod { get; set; }
        public string? Adi { get; set; }
        public string? Adres { get; set;}
        public ICollection<Bolum>? Bolums { get; set; }

    }
}
