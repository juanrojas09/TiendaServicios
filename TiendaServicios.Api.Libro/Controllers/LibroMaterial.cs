using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.Libro.Aplicacion;

namespace TiendaServicios.Api.Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroMaterial : ControllerBase
    {
        private readonly  IMediator _mediator;
        public LibroMaterial(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet("ConsultarLibros")]
        public async Task<ActionResult<List<LibroMaterialDTO>>> Consulta()
        {
            return await _mediator.Send(new Consultar.Ejecuta());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroMaterialDTO>> ConsultaFiltro(Guid id)
        {
            return await _mediator.Send(new ConsultaFiltro.LibroUnico { LibreriaMaterialId=id});
        }
    }
}
