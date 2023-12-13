using TiendaServicios.apo.CarritoCompra.RemoteModel;

namespace TiendaServicios.apo.CarritoCompra.RemoteInterface
{
    public interface ILibroService
    {
        public Task<(bool resultado,LibroRemote,string Error )> GetLibro(Guid LibroId);
    }
}
