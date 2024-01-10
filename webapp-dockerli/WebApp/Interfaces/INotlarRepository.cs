using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface INotlarRepository
    {
        ICollection<Notlar> GetNotlars();
        Notlar GetNotlar(int notID);
        bool NotlarExists(int ID);
        bool CreateNotlar(Notlar notlar);
        bool UpdateNotlar(Notlar notlar);
        bool DeleteNotlar(Notlar notlar);
        bool Save();
    }
}
