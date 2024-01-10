using System.ComponentModel;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IBolumRepository
    {
        ICollection <Bolum> GetBolums ();
        Bolum GetBolum (int bolumkod);
        bool BolumExists (int bolumKod);
        bool CreateBolum(Bolum bolum);
        bool UpdateBolum (Bolum bolum);
        bool DeleteBolum(Bolum bolum);
        bool Save();
    }
}
