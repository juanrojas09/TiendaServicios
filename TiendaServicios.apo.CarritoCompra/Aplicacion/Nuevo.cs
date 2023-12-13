using MediatR;
using TiendaServicios.apo.CarritoCompra.Modelo;
using TiendaServicios.apo.CarritoCompra.Persistencia;

namespace TiendaServicios.apo.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public DateTime FechaCreacionSesion { get; set; }
            public List<string> ProductoLista { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta>
        {

            private readonly CarritoContexto _carritoContexto;
            public Manejador(CarritoContexto carritoContexto)
            {
                _carritoContexto = carritoContexto;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion()
                {
                    FechaCreacion = request.FechaCreacionSesion,

                };
                _carritoContexto.CarritoSesiones.Add(carritoSesion);
                var value=await _carritoContexto.SaveChangesAsync();
                if (value == 0)
                {
                    throw new Exception("Errores en la insercion"); 
                }

               int id= carritoSesion.CarritoSesionId;

                foreach(var obj in request.ProductoLista)
                {
                    var detallesesion = new CarritoSesionDetalle()
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id,
                        ProductoSeleccionado = obj
                    };
                    _carritoContexto.CarritoSesionDetalles.Add(detallesesion);

                    
                }
                _carritoContexto.SaveChangesAsync();

                if (value > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("Error al crear detalle");
                
            }
        }
    }
}
