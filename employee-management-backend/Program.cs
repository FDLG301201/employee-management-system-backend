using Application.Services;
using Domain.Interfaces;
using employee_management_backend.Middleware;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configuración del DbContext - Lee exclusivamente desde appsettings.json o Azure Configuration
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Infrastructure"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Application Insights - Solo si existe la Connection String en configuración
//var appInsightsConnectionString = builder.Configuration.GetConnectionString("ApplicationInsights");
//if (!string.IsNullOrEmpty(appInsightsConnectionString))
//{
//    builder.Services.AddApplicationInsightsTelemetry();
//}

builder.Services.AddApplicationInsightsTelemetry();


//Inyeccion de Dependencias
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

//Configuracion de CORS - Orígenes específicos según entorno
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            // En desarrollo permitir orígenes locales
            policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        }
        else
        {
            // En producción usar la URL del frontend configurada en Azure
            var frontendUrl = builder.Configuration["FrontendUrl"];
            if (!string.IsNullOrEmpty(frontendUrl))
            {
                policy.WithOrigins(frontendUrl)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// NOTA: Swagger está habilitado en producción SOLO para facilitar la corrección de esta prueba técnica.
// En un escenario real de producción, Swagger debería estar deshabilitado por seguridad.
app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlingMiddleware>(); //USAR EL CUSTOM MIDDLEWARE DE MANEJO DE EXCEPCIONES

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins"); //USAR LA POLITICA DE CORS

app.UseAuthorization();

app.MapControllers();

app.Run();
