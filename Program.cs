using OrdenDeCompraP2.Data;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de la cadena de conexi�n para la base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configuraci�n de servicios HTTP para consumir la API externa de Pok�mon
builder.Services.AddHttpClient("PokemonAPI", client =>
{
    client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
});

// Configuraci�n de cliente HTTP para la API de facturaci�n
builder.Services.AddHttpClient("APIFacturacionClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7043/");
});

// Configuraci�n de controladores y Razor Pages
builder.Services.AddControllers();
builder.Services.AddRazorPages();

// Configuraci�n de memoria distribuida y sesiones
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Construcci�n de la aplicaci�n
var app = builder.Build();

// Configuraci�n del middleware de manejo de errores y redirecci�n HTTPS
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Configuraci�n de rutas, autorizaci�n y sesiones
app.UseRouting();
app.UseAuthorization();
app.UseSession();

// Mapeo de controladores y Razor Pages
app.MapControllers(); // Esto asegura que tus controladores API est�n activos y manejando rutas
app.MapRazorPages();

// Ejecuci�n de la aplicaci�n
app.Run();
