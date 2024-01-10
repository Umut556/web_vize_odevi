using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Interfaces;

namespace WebApp.Models
{
    [NotMapped]
    //[Keyless]
    public class Dersler : IEntity
    {
        [Key]
        public string? DersKodu { get; set; }
        public int Donemi { get; set; }
        public int Kredi { get; set; }
        public int ACTS { get; set; }
        public Donemler? Donemler { get; set; }
        public ICollection<Notlar>? Notlars { get; set; }

    }
}
