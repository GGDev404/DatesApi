using ApiDate_2._0.Db;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar el contexto de la base de datos primero
builder.Services.AddDbContext<DatesContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatesConnection"))
        .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
});
// Agregar otros servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configurar el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
