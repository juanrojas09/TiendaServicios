using Microsoft.EntityFrameworkCore;
using TiendaServicios.api.autor.Modelo;

namespace TiendaServicios.api.autor.Persistencia
{
    public class ContextoAutor:DbContext
    {
        //inicializo la conexion cuando se ejecuta el startup
        public ContextoAutor(DbContextOptions<ContextoAutor>options):base(options)
        {
            
        }
        
        public DbSet<AutorLibro> autorLibros { get; set; }
        public DbSet<GradoAcademico> gradoAcademicos { get; set; }
    }
}
