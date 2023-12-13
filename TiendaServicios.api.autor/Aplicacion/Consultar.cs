using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.api.autor.Modelo;
using TiendaServicios.api.autor.Persistencia;

namespace TiendaServicios.api.autor.Aplicacion
{
    public class Consultar
    {
        public class ListaAutor : IRequest<List<AutorDTO>>
        {
        }

        public  class Manejador : IRequestHandler<ListaAutor, List<AutorDTO>>
        {
            private readonly ContextoAutor _ContextoAutor;
            private readonly IMapper _mapper;
            public Manejador(ContextoAutor contextoAutor, IMapper mapper)
            {
                _ContextoAutor = contextoAutor;
                _mapper = mapper;
            }
            public async Task<List<AutorDTO>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {
                List<AutorLibro> Autores= await _ContextoAutor.autorLibros.ToListAsync();
                var autoresDto = _mapper.Map<List<AutorLibro>, List<AutorDTO>>(Autores);
                return autoresDto;
            }
        }
    }
}
