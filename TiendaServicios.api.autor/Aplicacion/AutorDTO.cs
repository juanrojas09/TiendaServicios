using TiendaServicios.api.autor.Modelo;

namespace TiendaServicios.api.autor.Aplicacion
{
    public class AutorDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }

      

        //id universal para comunicacion entre microservicios
        public string AutorLibroGuid { get; set; }
    }
    
}
