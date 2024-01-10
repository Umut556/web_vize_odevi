using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApp.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repository
{
    public class NotlarRepository : INotlarRepository
    {
        private readonly DataContext _context;
        public NotlarRepository(DataContext context)
        {

            _context = context;
        }

        public bool CreateNotlar(Notlar notlar)
        {
            _context.Add(notlar);
            _context.SaveChanges();
            return Save();
        }

        public bool DeleteNotlar(Notlar notlar)
        {
            _context.Remove(notlar);
            return Save();
        }

        public Notlar GetNotlar(int notID)
        {
            return _context.Notlars.Where(p => p.ID == notID).FirstOrDefault();
        }

        public ICollection<Notlar> GetNotlars()
        {
            return _context.Notlars.OrderBy(p => p.ID).ToList();
        }

        public bool NotlarExists(int ID)
        {
            return _context.Notlars.Any(p => p.ID == ID);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateNotlar(Notlar notlar)
        {
            _context.Update(notlar);
            return Save();
        }
    }
}
