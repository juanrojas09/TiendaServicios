using FluentValidation;
using MediatR;
using System.Drawing;
using TiendaServicios.api.autor.Modelo;
using TiendaServicios.api.autor.Persistencia;

namespace TiendaServicios.api.autor.Aplicacion
{
    public class Nuevo
    {
        //clase que recibe los parametros del controlador
        public class Ejecuta: IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }

        public class EjecutaValidacion:AbstractValidator<Ejecuta> {

            public EjecutaValidacion()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();

            }
        }


        //clase donde maneja la logica para manejar la request e interactuar con la db
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoAutor _ContextoAutor;
            public Manejador(ContextoAutor contextoAutor)
            {
                _ContextoAutor = contextoAutor;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try {
                    var autorLibro = new AutorLibro()
                    {
                        Nombre = request.Nombre,
                        Apellido = request.Apellido,
                        FechaNacimiento = request.FechaNacimiento,
                        AutorLibroGuid=Guid.NewGuid().ToString()
                    };

                    _ContextoAutor.autorLibros.Add(autorLibro);
                    var valor= await _ContextoAutor.SaveChangesAsync();
                    if (valor > 0)
                    {
                        return Unit.Value;
                    }

                    throw new Exception("Error al insertar un autor");
                }
                catch
                {
                    throw new Exception("Error al insertar un autor");
                }

            }
        }
    }
}
