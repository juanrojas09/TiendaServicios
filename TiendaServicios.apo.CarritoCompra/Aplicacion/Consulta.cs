using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.apo.CarritoCompra.Persistencia;
using TiendaServicios.apo.CarritoCompra.RemoteInterface;

namespace TiendaServicios.apo.CarritoCompra.Aplicacion
{
    public class Consulta
    {

        public class Ejecuta : IRequest<CarritoDTO>
        {
            public int CarritoSesionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDTO>
        {
            public readonly ILibroService _libroService;
            private readonly CarritoContexto _carritoContexto;
            public Manejador(ILibroService libroService, CarritoContexto carritoContexto)
            {
                _libroService = libroService;
                _carritoContexto = carritoContexto;
            }
            public async Task<CarritoDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //recopilo los datos del carrito sesion de mysql
                var carritoSesion = await _carritoContexto.CarritoSesiones.FirstOrDefaultAsync(x => x.CarritoSesionId == request.CarritoSesionId);
                var carritoSesionDetalle = await _carritoContexto.CarritoSesionDetalles.Where(x => x.CarritoSesionId == request.CarritoSesionId).ToListAsync();
                //para traaer el detalle de cada producto invoco al servicio que le pegamos al otro microservicio

                var listaCarritoDTO = new List<CarritoDetalleDTO>();
                foreach(var obj in carritoSesionDetalle)
                {
                    var productoDetalleResponse = await _libroService.GetLibro(new Guid(obj.ProductoSeleccionado));
                    if (productoDetalleResponse.resultado)
                    {
                        var objetoLibro = productoDetalleResponse.Item2;
                        var carritoDetalle = new CarritoDetalleDTO()
                        {
                            TituloLibro = objetoLibro.Titulo,
                            FechaPublicacion = objetoLibro.FechaPublicacion,
                            LibroId = objetoLibro.LibreriaMaterialId

                        };
                        listaCarritoDTO.Add(carritoDetalle);
                    }
                    
                    
                }
                var carritoSesionDto = new CarritoDTO()
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion,
                    ListaProductos = listaCarritoDTO
                };
                return carritoSesionDto;
                
            }
        }
    }
}
