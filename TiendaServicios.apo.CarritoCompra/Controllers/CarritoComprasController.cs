using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.apo.CarritoCompra.Aplicacion;
using TiendaServicios.apo.CarritoCompra.Modelo;

namespace TiendaServicios.apo.CarritoCompra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoComprasController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CarritoComprasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CrearSesion")]
        
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoDTO>> GetCarrito(int id)
        {
            return await _mediator.Send(new Consulta.Ejecuta{ CarritoSesionId=id});
        }
    }
}
