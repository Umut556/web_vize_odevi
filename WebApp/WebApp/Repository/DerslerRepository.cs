using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApp.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repository
{
    public class DerslerRepository : IDerslerRepository
    {
        private readonly DataContext _context;
        public DerslerRepository(DataContext context)
        {

            _context = context;
        }

        public bool CreateDersler(Dersler dersler)
        {
            _context.Add(dersler);
            _context.SaveChanges();
            return Save();
        }

        public bool DeleteDersler(Dersler dersler)
        {
            _context.Remove(dersler);
            return Save();
        }

        public bool DerslerExists(string DersKodu)
        {
            return _context.Derslers.Any(p => p.DersKodu == DersKodu);
        }

        public Dersler GetDersler(string DersKod)
        {
            return _context.Derslers.Where(p => p.DersKodu == DersKod).FirstOrDefault();
        }

        public ICollection<Dersler> GetDerslers()
        {
            return _context.Derslers.OrderBy(p => p.DersKodu).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateDersler(Dersler dersler)
        {
            _context.Update(dersler);
            return Save();
        }
    }
}
