using Microsoft.EntityFrameworkCore;
using PWA_WEBApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// TODO: Agregar la conexion a la DB
// 2. Conexion a sql server
const string CONNECTION_NAME = "devConnection";

// Obtener la cadena de conexion del appsetting
var connectionString = builder.Configuration.GetConnectionString(CONNECTION_NAME);

// Agregar contexto
builder.Services.AddDbContext<EquipoContext>(options => options.UseSqlServer(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
