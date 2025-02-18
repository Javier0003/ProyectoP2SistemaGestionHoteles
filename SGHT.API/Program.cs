using Microsoft.EntityFrameworkCore;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGH.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<SGHTContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SHGTDB")));

            builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();

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