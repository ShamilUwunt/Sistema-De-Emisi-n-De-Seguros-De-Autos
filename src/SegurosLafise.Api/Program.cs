using Microsoft.EntityFrameworkCore;
using SegurosLafise.Api.Data;
using SegurosLafise.Api.Repositories;
using SegurosLafise.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Conexion a SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositorios (capa de datos)
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ICoberturaRepository, CoberturaRepository>();
builder.Services.AddScoped<IPolizaRepository, PolizaRepository>();

// Servicios (capa de logica de negocio)
builder.Services.AddScoped<ICatalogoService, CatalogoService>();
builder.Services.AddScoped<IPolizaService, PolizaService>();

builder.Services.AddControllers();

// Swagger para probar la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger disponible siempre para facilitar las pruebas
app.UseSwagger();
app.UseSwaggerUI();

// Servir el frontend simple (wwwroot/index.html)
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
