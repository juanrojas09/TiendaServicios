using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.Api.Libro.Persistencia
{
    public class ContextoLibreria:DbContext
    {
        public ContextoLibreria()
        {
            
        }
        public ContextoLibreria(DbContextOptions<ContextoLibreria> options) : base(options)
        {
            
        }


        //El virtual significa q se va a poder sobrescribir a futuro

        public virtual DbSet<LibreriaMaterial> LibreriaMaterial { get; set; }
    }
}
