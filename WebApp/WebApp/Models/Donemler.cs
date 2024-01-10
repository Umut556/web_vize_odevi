using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Interfaces;

namespace WebApp.Models
{
    [NotMapped]
    //[Keyless]
    public class Donemler :IEntity
    {
        [Key]
        public int donemKod { get; set; }
        public string? donem { get; set; }
        public ICollection<Dersler>? Derslers { get; set; }
    }
}
