using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Interfaces;

namespace WebApp.Models
{
    [NotMapped]
    //[Keyless]
    public class Notlar : IEntity
    {
        public int ID { get; set; }
        public string? ogrno { get; set; }
        public string? derskod { get; set;}
        public int yil { get; set; }
        public int vizesi { get; set; }
        public int finali { get; set; }
        public int butunleme { get; set; }
        public int ortalama { get; set; }
        public Dersler? Dersler { get; set; }
        public Ogrenci? Ogrenci { get; set; }


    }
}
