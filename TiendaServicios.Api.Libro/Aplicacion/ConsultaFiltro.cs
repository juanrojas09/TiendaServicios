using AutoMapper;
using Azure.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class ConsultaFiltro
    {
        public class LibroUnico:IRequest<LibroMaterialDTO>
        {
            public Guid? LibreriaMaterialId { get; set; }
        }

        public class Manejador : IRequestHandler<LibroUnico, LibroMaterialDTO>
        {

            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;
            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }
            public async Task<LibroMaterialDTO> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await _contexto.LibreriaMaterial.FirstOrDefaultAsync(x => x.LibreriaMaterialId == request.LibreriaMaterialId);
                var LibroDto = _mapper.Map<LibreriaMaterial, LibroMaterialDTO>(libro);
                if (libro == null)
                {
                    throw new Exception("Error al traer liibros");
                }
                return LibroDto;
            }
        }


    }
}
