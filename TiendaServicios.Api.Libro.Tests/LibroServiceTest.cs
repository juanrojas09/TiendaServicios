using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;
using Xunit;
namespace TiendaServicios.Api.Libro.Tests
{
    public class LibroServiceTest
    {

        private IEnumerable<LibreriaMaterial> ObtenerDataPrueba()
        {
            //retornar data de prueba
            //EL genfu genera data falsa pero persisstente a la db para probar un endpoint o logica de un endpoint
            //Genero un titulo, un guid y lleno 30 registros
            A.Configure<LibreriaMaterial>().Fill(x => x.Titulo).AsArticleTitle()
                .Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<LibreriaMaterial>(30);
            lista[0].LibreriaMaterialId = Guid.Empty;
            return lista;
        }

        /// <summary>
        /// Mock del contexto y de la entidad
        /// </summary>
        /// <returns></returns>
        private Mock<ContextoLibreria> CrearContexto()
        {
            var dataPrueba = ObtenerDataPrueba().AsQueryable();
            //Indico que la clase debe ser de tipo entidad, es la propiedad que tiene que tener una clase de entity framework que consumira la data de prueba
            var dbSet = new Mock<DbSet<LibreriaMaterial>>();
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());
            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken())).Returns(new AsyncEnumerator<LibreriaMaterial>(dataPrueba.GetEnumerator()));

            //Agrego para poder hacer los filtros referidos a la entidad mockeada, las clases genericas son la implementacion de las interfaces que usa EF core
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibreriaMaterial>(dataPrueba.Provider));


            var contexto = new Mock<ContextoLibreria>();
            contexto.Setup(x => x.LibreriaMaterial).Returns(dbSet.Object);
            return contexto;
        }



        [Fact]
        public async void GetLibro_x_Id()
        {
            var mockContexto = CrearContexto();
            var mapConfig = new MapperConfiguration(cfg =>

            {
                cfg.AddProfile(new MappingTest());
            });
            var mapper = mapConfig.CreateMapper();
            var request = new ConsultaFiltro.LibroUnico();
            request.LibreriaMaterialId = Guid.Empty;
            var manejador = new ConsultaFiltro.Manejador(mockContexto.Object, mapper);
            var libro = await manejador.Handle(request, new System.Threading.CancellationToken());
            Assert.NotNull(libro);
            Assert.True(libro.LibreriaMaterialId == Guid.Empty);
        }

        [Fact]
        public async void GetLibros()
        {
            System.Diagnostics.Debugger.Launch();
            //que metodo de mi microservicio es el encargado de realizar la consulta de libros en la DB
            //1. emular la instancia de EF core
            //para emular acciones y eventos de un objeto en en ambiente de UT usamos objetos de tipo MOCK (representacion de objetos)
            //instalamos mock
            var mockContext = CrearContexto();

            //Emular al mapping
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());

            });

            var mapper = mapConfig.CreateMapper();

            //3 instancio a manejador

            Consultar.Manejador manejador = new Consultar.Manejador(mockContext.Object, mapper);

            Consultar.Ejecuta requst = new Consultar.Ejecuta();

            var Lista = await manejador.Handle(requst, new System.Threading.CancellationToken());
            Assert.True(Lista.Any());



        }


        [Fact]

        public async void GuardarLibro()
        {
            var options = new DbContextOptionsBuilder<ContextoLibreria>().UseInMemoryDatabase(databaseName: "BaseDeDatosLibro").Options;
            var contexto = new ContextoLibreria(options);

            var request = new Nuevo.Ejecuta()
            {
                Titulo = "Libro de microservice",
                AutorLibro = Guid.Empty,
                FechaPublicacion = DateTime.Now
            };
            var manejador = new Nuevo.Manejador(contexto);
            var libro = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(libro != null);

        } 

    }
}
