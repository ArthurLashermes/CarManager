using Microsoft.EntityFrameworkCore;
using Serilog;
using Server.Factory;
using Server.Services;
using System.Text.Json.Serialization;
using Server.Middleware;
using WebApplication1;
using Path = System.IO.Path;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));


builder.Services.AddDbContext<ApplicationDbContext>(
	options => options.UseSqlite("Data Source=Database.db;")); // Avec cette option, la BDD se retrouve dans le chemin bin/debug, on evite d'utiliser un chemin absolu
// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    var filePath = Path.Combine(AppContext.BaseDirectory, "WebApi.xml");
    c.IncludeXmlComments(filePath);
});

builder.Services.AddScoped<BrandFactory>();
builder.Services.AddScoped<CarFactory>();
builder.Services.AddScoped<VehicleFactory>();
builder.Services.AddScoped<MaintenanceFactory>(); 

builder.Services.AddScoped<MaintenanceService>();
builder.Services.AddScoped<VehicleService>();

var app = builder.Build();

app.UseErrorHandlingMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
