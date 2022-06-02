using Ibdb.Books.Infrastructure.Ef;
using Ibdb.Shared;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<BooksContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Books")));
builder.Services.AddSharedServices(options => options
    .SetEventStoreConnectionString(builder.Configuration.GetConnectionString("EventStore"))
    .SetOutboxConnectionString(builder.Configuration.GetConnectionString("Outbox"))
    .SetRabbitMqConnectionString(builder.Configuration.GetConnectionString("RabbitMq")));
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
