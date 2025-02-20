using Microsoft.EntityFrameworkCore;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces.Configuration;
using SGHT.Persistance.Interfaces.Reservation;
using SGHT.Persistance.Interfaces.Security;
using SGHT.Persistance.Repositories.Configuration;
using SGHT.Persistance.Repositories.Reservation;
using SGHT.Persistance.Repositories.Security;

namespace SGH.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<SGHTContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SHGTDB")));

            builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
            builder.Services.AddScoped<IEstadoHabitacionRepository, EstadoHabitacionRepository>();
            builder.Services.AddScoped<IHabitacionRepository, HabitacionRepository>();
            builder.Services.AddScoped<IPisoRepository, PisoRepository>();
            builder.Services.AddScoped<IRecepcionRepository, RecepcionRepository>();
            builder.Services.AddScoped<IRolUsuarioRepository, RolUsuarioRepository>();
            builder.Services.AddScoped<IServiciosRepository, ServiciosRepository>();
            builder.Services.AddScoped<ITarifasRepository, TarifasRepository>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}