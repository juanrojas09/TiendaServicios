﻿namespace TiendaServicios.apo.CarritoCompra.Modelo
{
    public class CarritoSesion
    {
        public int CarritoSesionId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public ICollection<CarritoSesionDetalle> carritoSesionDetalles { get; set; }
    }
}
