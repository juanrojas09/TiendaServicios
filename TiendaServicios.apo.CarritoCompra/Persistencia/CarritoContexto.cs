using Microsoft.EntityFrameworkCore;
using TiendaServicios.apo.CarritoCompra.Modelo;

namespace TiendaServicios.apo.CarritoCompra.Persistencia
{
    public class CarritoContexto:DbContext
    {
        public CarritoContexto(DbContextOptions<CarritoContexto> options) : base(options)
        {

        }

        public DbSet<CarritoSesion> CarritoSesiones { get; set; }
        public DbSet<CarritoSesionDetalle> CarritoSesionDetalles { get; set; }
    }
}
