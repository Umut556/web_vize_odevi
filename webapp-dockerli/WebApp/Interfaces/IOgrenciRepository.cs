using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IOgrenciRepository
    {
        ICollection<Ogrenci> GetOgrencis();
        Ogrenci GetOgrenci(string no);
        bool OgrenciExists(string OgrNo);
        bool CreateOgrenci(Ogrenci ogrenci);
        bool UpdateOgrenci(Ogrenci ogrenci);
        bool DeleteOgrenci(Ogrenci ogrenci);
        bool Save();
    }
}
