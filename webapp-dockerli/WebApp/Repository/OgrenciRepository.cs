using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApp.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repository
{
    public class OgrenciRepository : IOgrenciRepository
    {
        private readonly DataContext _context;
        public OgrenciRepository(DataContext context)
        {

            _context = context;
        }

        public bool CreateOgrenci(Ogrenci ogrenci)
        {
            _context.Add(ogrenci);
            _context.SaveChanges();
            return Save();
        }

        public bool DeleteOgrenci(Ogrenci ogrenci)
        {
            _context.Remove(ogrenci);
            return Save();
        }

        public Ogrenci GetOgrenci(string no)
        {
            return _context.Ogrencis.Where(p => p.OgrNo == no).FirstOrDefault();
        }

        public ICollection<Ogrenci> GetOgrencis()
        {
            return _context.Ogrencis.OrderBy(p => p.OgrNo).ToList();
        }

        public bool OgrenciExists(string OgrNo)
        {
            return _context.Ogrencis.Any(p => p.OgrNo == OgrNo);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOgrenci(Ogrenci ogrenci)
        {
            _context.Update(ogrenci);
            return Save();
        }
    }
}
