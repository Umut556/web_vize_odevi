using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApp.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repository
{
    public class DonemlerRepository : IDonemlerRepository
    {
        private readonly DataContext _context;
        public DonemlerRepository(DataContext context)
        {

            _context = context;
        }
        public bool DonemlerExists(int donemKodu)
        {
            return _context.Donemlers.Any(p => p.donemKod == donemKodu);
        }

        public ICollection<Donemler> GetDonemlers()
        {
            return _context.Donemlers.OrderBy(p => p.donemKod).ToList();
        }

        public Donemler GetDonemler(int Donemkod)
        {
            return _context.Donemlers.Where(p => p.donemKod == Donemkod).FirstOrDefault();
        }

        public bool CreateDonemler(Donemler donemler)
        {
            _context.Add(donemler);
            _context.SaveChanges();
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateDonemler(Donemler donemler)
        {
            _context.Update(donemler);
            return Save();
        }

        public bool DeleteDonemler(Donemler donemler)
        {
            _context.Remove(donemler);
            return Save();
        }
    }
}
