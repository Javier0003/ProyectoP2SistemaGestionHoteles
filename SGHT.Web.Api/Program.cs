using SGHT.Web.Api.Controllers.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register HTTP client with base URL from configuration
builder.Services.AddHttpClient<IHttpClientService, HttpClientService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["baseUrl"]);
});

// Register error handler
builder.Services.AddScoped<IErrorHandler, ErrorHandler>();

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
