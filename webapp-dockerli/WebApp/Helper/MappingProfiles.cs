using AutoMapper;
using WebApp.Dto;
using WebApp.Models;

namespace WebApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Bolum, BolumDto>();
            CreateMap<BolumDto,Bolum>();

            CreateMap<Dersler, DerslerDto>();
            CreateMap<DerslerDto,Dersler >();

            CreateMap<Donemler, DonemlerDto>();
            CreateMap<DonemlerDto,Donemler >();

            CreateMap<Fakulte, FakulteDto>();
            CreateMap<FakulteDto,Fakulte >();

            CreateMap<Notlar, NotlarDto>();
            CreateMap<NotlarDto,Notlar >();

            CreateMap<Ogrenci, OgrenciDto>();
            CreateMap<OgrenciDto,Ogrenci >();
        }

    }
}
