using System.Text.Json;
using System.Text.Json.Serialization;
using TiendaServicios.apo.CarritoCompra.RemoteInterface;
using TiendaServicios.apo.CarritoCompra.RemoteModel;

namespace TiendaServicios.apo.CarritoCompra.RemoteServices
{
    public class LibroServices : ILibroService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<LibroServices> _logger;
        public LibroServices(IHttpClientFactory httpClient, ILogger<LibroServices> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Metodo para hacer una comunicacion sincrona entre dos microservicios
        /// </summary>
        /// <param name="LibroId"></param>
        /// <returns></returns>
        public async Task<(bool resultado, LibroRemote, string Error)> GetLibro(Guid LibroId)
        {
            try
            {
                //rescato el cliente de lo implementado en el startup
                var cliente = _httpClient.CreateClient("Libros");
                //consumo el endpoint pasandole la url del controller y el detalle del endpoint y el body param
                var resp=await cliente.GetAsync($"api/LibroMaterial/{LibroId}");
                if (resp.IsSuccessStatusCode)
                {
                    var contenido = await resp.Content.ReadAsStringAsync();
                    //Lo serializo para luego deserializarlo y que haga match con el RemoteDTO que setie antes
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var res = JsonSerializer.Deserialize<LibroRemote>(contenido, options);
                    return (true, res, null);
                }
                else
                {
                    return (false, null, resp.ReasonPhrase);
                }
                
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}
