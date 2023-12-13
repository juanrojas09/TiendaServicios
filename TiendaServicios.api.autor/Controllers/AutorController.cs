using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.api.autor.Aplicacion;
using TiendaServicios.api.autor.Modelo;

namespace TiendaServicios.api.autor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
      
        private IMediator _mediator;
        public AutorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateAutor")]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);

        }

        [HttpGet("GetAutores")]
        public async Task<ActionResult<List<AutorDTO>>> Consulta()
        {
            return await _mediator.Send(new Consultar.ListaAutor());
        }

        [HttpGet("GetAutoresById")]
        public async Task<ActionResult<AutorDTO>> ConsultarFiltro(string id)
        {
            return await _mediator.Send(new ConsultaFiltro.AutorUnico{ AutorGuid=id });
        }
    }
}
