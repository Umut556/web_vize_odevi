using WebApp.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repository
{
    public class BolumRepository : IBolumRepository
    {
        private readonly DataContext _context;
        public BolumRepository(DataContext context) {
            
            _context = context;
        }

        public bool BolumExists(int bolumKod)
        {
            return _context.Bolums.Any(p => p.BolumKod == bolumKod);
        }

        public bool CreateBolum(Bolum bolum)
        {

            _context.Add(bolum);
            _context.SaveChanges();
            return Save();

        }

        public bool DeleteBolum(Bolum bolum)
        {
            _context.Remove(bolum);
            return Save();
        }

        public Bolum GetBolum(int bolumkod)
        {
            return _context.Bolums.Where(p => p.BolumKod == bolumkod).FirstOrDefault();
        }

        public ICollection<Bolum> GetBolums()
        {
            return _context.Bolums.OrderBy(p => p.BolumKod).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateBolum(Bolum bolum)
        {
            _context.Update(bolum);
            return Save();
        }
    }
}
