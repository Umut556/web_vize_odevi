using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IDerslerRepository
    {
        ICollection<Dersler> GetDerslers();
        Dersler GetDersler(string DersKod);
        bool DerslerExists(string DersKodu);
        bool CreateDersler(Dersler dersler);
        bool UpdateDersler(Dersler dersler);
        bool DeleteDersler(Dersler dersler);
        bool Save();
    }
}
