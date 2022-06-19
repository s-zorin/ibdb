using Ibdb.Reviews.Infrastructure.Ef;
using Ibdb.Shared;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ReviewsContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Reviews")));
builder.Services.AddSharedServices(options => options
    .AddEventStore(builder.Configuration.GetConnectionString("EventStore"))
    .AddOutbox(builder.Configuration.GetConnectionString("Outbox"))
    .AddRabbitMq(builder.Configuration.GetConnectionString("RabbitMq")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
