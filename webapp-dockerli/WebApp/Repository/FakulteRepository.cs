using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApp.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repository
{
    public class FakulteRepository : IFakulteRepository
    {
        private readonly DataContext _context;
        public FakulteRepository(DataContext context)
        {

            _context = context;
        }

        public bool CreateFakulte(Fakulte fakulte)
        {
            _context.Add(fakulte);
            _context.SaveChanges();
            return Save();
        }

        public bool DeleteFakulte(Fakulte fakulte)
        {
            _context.Remove(fakulte);
            return Save();
        }

        public bool FakulteExists(int FKKod)
        {
            return _context.Fakultes.Any(p => p.FKKod == FKKod);
        }

        public Fakulte GetFakulte(int FKkod)
        {
            return _context.Fakultes.Where(p => p.FKKod == FKkod).FirstOrDefault();
        }

        public ICollection<Fakulte> GetFakultes()
        {
            return _context.Fakultes.OrderBy(p => p.FKKod).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateFakulte(Fakulte fakulte)
        {
            _context.Update(fakulte);
            return Save();
        }
    }
}
