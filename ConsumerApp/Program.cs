using ConsumerApp.ConsumerComponents;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                .AddTransient<IRequestHandler<LogCommand, Unit>, LogCommandHandler>()
                .AddHostedService<LogConsumer>()
                .AddSingleton(serviceProvider =>
                {
                    return new ConnectionFactory
                    {
                        HostName="rabbitmq",
                        UserName="guest",
                        Password="guest",
                        VirtualHost="/",
                        DispatchConsumersAsync = true
                    };
                });
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();

