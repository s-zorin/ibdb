using AutoMapper;
using EasyNetQ;
using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Filters;
using Ibdb.Shared.Application.Handlers;
using Ibdb.Shared.Infrastructure;
using Ibdb.Shared.Infrastructure.Ef;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ibdb.Shared
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services, Action<SharedServicesOptions> configurationAction)
        {
            var options = new SharedServicesOptions();
            configurationAction(options);
            return services.AddSharedServices(options);
        }

        public static IServiceCollection AddSharedServices(this IServiceCollection services, SharedServicesOptions options)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var entryAssembly = Assembly.GetEntryAssembly()!;

            services.Scan(scan => scan
                .FromAssemblies(executingAssembly, entryAssembly)
                    .AddClasses(classes => classes
                        .AssignableTo<ITransientService>())
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                     .AddClasses(classes => classes
                        .AssignableTo<IScopedService>())
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()
                     .AddClasses(classes => classes
                        .AssignableTo<ISingletonService>())
                        .AsImplementedInterfaces()
                        .WithSingletonLifetime()
                     .AddClasses(classes => classes
                        .AssignableTo(typeof(ICommandHandler<>))
                        .Where(t => t != typeof(RetryCommandHandlerDecorator<>)))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                     .AddClasses(classes => classes
                        .AssignableTo(typeof(IRequestHandler<,>)))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                     .AddClasses(classes => classes
                        .AssignableTo(typeof(INotificationHandler<>)))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                     .AddClasses(classes => classes
                        .AssignableTo(typeof(IEventHandler<,>)))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                     .AddClasses(classes => classes
                        .AssignableTo(typeof(IRepository<>)))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()
                     .AddClasses(classes => classes
                        .AssignableTo(typeof(IIntegrationEventHandler<>)))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                     .AddClasses(classes => classes
                        .AssignableTo(typeof(IQueryHandler<,>)))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());

            var mapper = new MapperConfiguration(c => c.AddMaps(executingAssembly, entryAssembly)).CreateMapper();
            services.AddTransient<Application.IMapper, AutoMapperMapper>(_ => new AutoMapperMapper(mapper));

            services.TryDecorate(typeof(ICommandHandler<>), typeof(RetryCommandHandlerDecorator<>));

            if (options.EventStoreConnectionString is not null)
                services.AddDbContext<EventStoreContext>(ctxOptions => ctxOptions.UseNpgsql(options.EventStoreConnectionString));

            if (options.OutboxConnectionString is not null)
            {
                services.AddDbContext<OutboxContext>(ctxOptions => ctxOptions.UseNpgsql(options.OutboxConnectionString));
                services.AddHostedService<OutboxBackroundService>();
            }

            if (options.RabbitMqConnectionString is not null)
            {
                services.AddSingleton(RabbitHutch.CreateBus(options.RabbitMqConnectionString));
                services.AddHostedService(serviceProvider => new RabbitMqBackgroundService(serviceProvider, services));
            }

            services.AddAutoMapper(executingAssembly, entryAssembly);

            services.AddSignalR();

            services
                .AddControllers(options => options.Filters.Add<CommonResultFilter>())
                .ConfigureApplicationPartManager(manager => manager.ApplicationParts.Add(new AssemblyPart(typeof(ServiceCollectionExtensions).Assembly)));

            return services;
        }
    }
}
