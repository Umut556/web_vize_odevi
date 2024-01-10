using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IDonemlerRepository
    {
        ICollection<Donemler> GetDonemlers();
        Donemler GetDonemler(int Donemkod);
        bool DonemlerExists(int donemKodu);
        bool CreateDonemler(Donemler donemler);
        bool UpdateDonemler(Donemler donemler);
        bool DeleteDonemler(Donemler donemler);
        bool Save();
    }
}
