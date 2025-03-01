using Microsoft.EntityFrameworkCore;
using SGHT.Persistance.Context;
using SGHT.IOC.Dependancies;

namespace SGH.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<SGHTContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SHGTDB")));

            builder.Services.AddRolUsuarioDependancy();
            builder.Services.AddCategoriaDependancy();
            builder.Services.AddCLienteDependancy();
            builder.Services.AddEstadoHabitacionDependancy();
            builder.Services.AddHabitacionDependancy();
            builder.Services.AddPisoDependancy();
            builder.Services.AddRecepcionDependancy();
            builder.Services.AddRolUsuarioDependancy();
            builder.Services.AddServiciosDependancy();
            builder.Services.AddTarifasDependancy();
            builder.Services.AddUsuariosDependancy();

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