using Microsoft.EntityFrameworkCore;
using SGHT.IOC.Dependancies;
using SGHT.Persistance.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SGHTContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SHGTDB")));

builder.Services.AddRolUsuarioDependancy();
builder.Services.AddCategoriaDependancy();
builder.Services.AddCLienteDependancy();
builder.Services.AddEstadoHabitacionDependancy();
builder.Services.AddHabitacionDependancy();
builder.Services.AddPisoDependancy();
builder.Services.AddRecepcionDependancy();
builder.Services.AddServiciosDependancy();
builder.Services.AddTarifasDependancy();
builder.Services.AddUsuariosDependancy();
builder.Services.AddJwtAuthenticationDependancy();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
