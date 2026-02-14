using API_AutomaG.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<API_AutomaGContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("API_AutomaG_Postgres")
        ?? throw new InvalidOperationException("Connection string not found.")
    )
);



// 🔹 CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMVC",
        policy =>
        {
            policy.WithOrigins("https://localhost:7002")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔹 Activar Swagger siempre, incluso en producción
app.UseSwagger();

// 🔹 Configurar Swagger UI para que se abra en la raíz
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Automagest API V1");
    c.RoutePrefix = ""; // Esto hace que Swagger se abra en /
});

app.UseHttpsRedirection();

// 🔹 CORS VA AQUÍ
app.UseCors("AllowMVC");

app.UseAuthorization();

app.MapControllers();

app.Run();
