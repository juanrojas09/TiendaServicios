using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Consultar
    {
        public class Ejecuta:IRequest<List<LibroMaterialDTO>>
        {

        }


        public class Manejador : IRequestHandler<Ejecuta, List<LibroMaterialDTO>>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;
            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }
            public async Task<List<LibroMaterialDTO>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libro = await _contexto.LibreriaMaterial.ToListAsync();
             
                    var LibroDTO = _mapper.Map<List<LibreriaMaterial>, List<LibroMaterialDTO>>(libro);
                if(libro.Count == 0)
                {
                    throw new Exception("Error, no hay libros");
                }
                return LibroDTO;

              
            }
        }
    }
}
