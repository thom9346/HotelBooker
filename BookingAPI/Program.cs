using BookingApi.Data;
using BookingApi.Infrastructure;
using BookingApi.Models;
using BookingApi.Services;
using Microsoft.EntityFrameworkCore;
using SharedModels.Booking;

var builder = WebApplication.CreateBuilder(args);

// RabbitMQ connection string (see docker-compose.yml).
string ConnectionString = "host=rabbitmq";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BookingApiContext>(opt => opt.UseInMemoryDatabase("BookingDb"));

builder.Services.AddScoped<IRepository<Booking>, BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();


// Register database initializer for dependency injection
builder.Services.AddTransient<IDbInitializer, DbInitializer>();

// Register MessagePublisher (a messaging gateway) for dependency injection
builder.Services.AddSingleton<IMessagePublisher>(new
    MessagePublisher(ConnectionString));


// Dependency injection for converter
builder.Services.AddSingleton<IConverter<Booking, BookingDTO>, BookingConverter>();

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
    var dbContext = services.GetService<BookingApiContext>();
    var dbInitializer = services.GetService<IDbInitializer>();
    dbInitializer.Initialize(dbContext);
}

app.Run();
