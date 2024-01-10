using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Interfaces;

namespace WebApp.Models
{
    [NotMapped]
    public class Ogrenci : IEntity
    {
        [Key]
        public string? OgrNo { get; set; }
        public string? Adi { get; set; }
        public string? Soyadi { get; set; }
        public int Bolumu { get; set; }
        public string? Adres { get; set; }
        public int Il { get; set; } 

        public Bolum? Bolum { get; set; }
        public ICollection<Notlar>? Notlars { get; set; }
    }
}
