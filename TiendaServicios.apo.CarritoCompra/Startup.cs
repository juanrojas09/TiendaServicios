using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.apo.CarritoCompra.Aplicacion;
using TiendaServicios.apo.CarritoCompra.Persistencia;
using TiendaServicios.apo.CarritoCompra.RemoteInterface;
using TiendaServicios.apo.CarritoCompra.RemoteServices;

namespace TiendaServicios.apo.CarritoCompra
{
    public class Startup
    {
        public static WebApplication initializeApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            Configure(app);
            return app;
        }








        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            IServiceCollection services = new ServiceCollection();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            // Duplicate here any configuration sources you use.
            configurationBuilder.AddJsonFile("appsettings.json");
            IConfiguration configuration = configurationBuilder.Build();

            builder.Services.AddTransient<ILibroService, LibroServices>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMediatR(typeof(Nuevo.Manejador).Assembly);

            builder.Services.AddDbContext<CarritoContexto>(options =>
                 options.UseMySQL(configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());
            //builder.Services.AddAutoMapper(typeof(Consultar.Manejador));
            builder.Services.AddHttpClient("Libros", config =>
            {
                config.BaseAddress = new Uri(configuration["Services:Libros"]);
            });


            
        }



        private static void Configure(WebApplication app)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            // Duplicate here any configuration sources you use.
            configurationBuilder.AddJsonFile("AppSettings.json");
            IConfiguration configuration = configurationBuilder.Build();





            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseCors();
            app.MapControllers();


        }
    }
}
