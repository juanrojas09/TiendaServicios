using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.api.autor.Modelo;
using TiendaServicios.api.autor.Persistencia;

namespace TiendaServicios.api.autor.Aplicacion
{
    public class ConsultaFiltro
    {
        public class AutorUnico : IRequest<AutorDTO>
        {
            public string AutorGuid { get; set; }

        }
        public class Manejador : IRequestHandler<AutorUnico, AutorDTO>
        {
            private readonly ContextoAutor _ContextoAutor;
            private readonly IMapper _mapper;
            public Manejador(ContextoAutor contextoAutor, IMapper mapper)
            {
                _ContextoAutor = contextoAutor;
                _mapper = mapper;
            }
            public async Task<AutorDTO> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var Autor = await _ContextoAutor.autorLibros.FirstOrDefaultAsync(x => x.AutorLibroGuid == request.AutorGuid);
                var AutorDto = _mapper.Map<AutorLibro, AutorDTO>(Autor);
                if (AutorDto == null)
                {
                    throw new Exception("No habian autores");
                }
                return AutorDto;
  
            }
        }
    }
}
