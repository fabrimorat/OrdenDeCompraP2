using OrdenDeCompraP2.Data;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la cadena de conexión para la base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configuración de servicios HTTP para consumir la API externa de Pokémon
builder.Services.AddHttpClient("PokemonAPI", client =>
{
    client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
});

// Configuración de cliente HTTP para la API de facturación
builder.Services.AddHttpClient("APIFacturacionClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7043/");
});

// Configuración de controladores y Razor Pages
builder.Services.AddControllers();
builder.Services.AddRazorPages();

// Configuración de memoria distribuida y sesiones
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Construcción de la aplicación
var app = builder.Build();

// Configuración del middleware de manejo de errores y redirección HTTPS
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Configuración de rutas, autorización y sesiones
app.UseRouting();
app.UseAuthorization();
app.UseSession();

// Mapeo de controladores y Razor Pages
app.MapControllers(); // Esto asegura que tus controladores API estén activos y manejando rutas
app.MapRazorPages();

// Ejecución de la aplicación
app.Run();
