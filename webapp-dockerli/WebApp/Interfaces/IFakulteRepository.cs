using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IFakulteRepository
    {
        ICollection<Fakulte> GetFakultes();
        Fakulte GetFakulte(int FKkod);
        bool FakulteExists(int FKKod);
        bool CreateFakulte(Fakulte fakulte);
        bool UpdateFakulte(Fakulte fakulte);
        bool DeleteFakulte(Fakulte fakulte);
        bool Save();

    }
}
