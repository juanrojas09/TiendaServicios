using AutoMapper;

namespace TiendaServicios.api.autor.Aplicacion
{
    public class MappinigProfile:Profile
    {
        public MappinigProfile()
        {
            CreateMap<Modelo.AutorLibro, AutorDTO>();
          
        }
    }
}
