using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Interfaces;

namespace WebApp.Models {
    [NotMapped]
    //[Keyless]
    public class Bolum : IEntity
    {
        [Key]
        public int BolumKod { get; set; } 
        public string? Adi { get; set; }
        public int Fakultesi { get;  set; }
        public Fakulte? Fakulte { get; set; }
        public ICollection<Ogrenci>? Ogrencis { get; set; }
    }
}
