using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Agregar controladores
builder.Services.AddEndpointsApiExplorer(); // Explorador de endpoints para Swagger
builder.Services.AddSwaggerGen(); // Generar Swagger
builder.Services.AddScoped<TaskService>();

//base de datois

builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
    new MySqlServerVersion(new Version(8, 0, 2)))); // Ajusta la versión según tu instalación
// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()  // Permitir cualquier origen
               .AllowAnyMethod()  // Permitir cualquier método (GET, POST, etc.)
               .AllowAnyHeader(); // Permitir cualquier cabecera
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Habilitar Swagger en desarrollo
    app.UseSwaggerUI(); // Interfaz de usuario de Swagger
}

app.UseHttpsRedirection(); // Redirección a HTTPS

app.UseAuthorization(); // Habilitar autorización

app.MapControllers(); // Mapear controladores

app.Run(); // Ejecutar la aplicación
