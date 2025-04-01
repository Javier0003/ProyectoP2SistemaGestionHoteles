using SGHT.IOC.Dependancies;
using SGHT.Web.Api.Controllers.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register HTTP client with base URL from configuration
builder.Services.AddHttpClientDependancy(builder.Configuration["baseUrl"]);
builder.Services.AddUsuarioHttpDependancy();
builder.Services.AddHttpTarifaDependancy();
builder.Services.AddHttpRolUsuarioDependancy();

// Register error handler
builder.Services.AddHttpErrorHandlerDependancy();

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
