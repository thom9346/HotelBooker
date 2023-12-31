using HotelRoomApi.Data;
using HotelRoomApi.Infrastructure;
using HotelRoomApi.Models;
using HotelRoomApi.Services;
using Microsoft.EntityFrameworkCore;
using SharedModels.HotelRoom;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// RabbitMQ connection string (see docker-compose.yml).
string ConnectionString = "host=rabbitmq";

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HotelRoomApiContext>(opt => opt.UseInMemoryDatabase("HotelRoomDb"));

builder.Services.AddScoped<IRepository<HotelRoom>, HotelRoomRepository>();
builder.Services.AddScoped<IHotelRoomService, HotelRoomService>();

// Register database initializer for dependency injection
builder.Services.AddTransient<IDbInitializer, DbInitializer>();

// Dependency injection for converter
builder.Services.AddSingleton<IConverter<HotelRoom, HotelRoomDTO>, HotelRoomConverter>();
var app = builder.Build();

// Create a message listener in a separate thread.
Task.Factory.StartNew(() =>
    new MessageListener(app.Services, ConnectionString).Start());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

// Initialize the database.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetService<HotelRoomApiContext>();
    var dbInitializer = services.GetService<IDbInitializer>();
    dbInitializer.Initialize(dbContext);
}

app.Run();
