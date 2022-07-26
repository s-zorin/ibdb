using Ibdb.Notifications.Application.Hubs;
using Ibdb.Shared;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSharedServices(options => options
    .AddRabbitMq(builder.Configuration.GetConnectionString("RabbitMq")));

var app = builder.Build();
app.MapHub<NotificationHub>("api/notifications");
app.Run();