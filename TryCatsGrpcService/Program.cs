﻿using BusinessLogicLayer;
using BusinessLogicLayer.Finder;
using BusinessLogicLayer.Repository;
using DataAccessLayer;
using DataAccessLayer.Finder;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using TryCatsGrpcService.Services;

namespace TryCatsGrpcService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.ConfigureAppConfiguration(config =>
            {
                var prefix = "CATSGRPC_";
                config.AddEnvironmentVariables(prefix);
            });

            var configuration = builder.Configuration;

            // Additional configuration is required to successfully run gRPC on macOS.
            // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

            string con = "Data Source=Cats.db";
            // устанавливаем контекст данных
            //builder.Services.AddDbContext<CatsContext>(options => options.UseSqlite(con));

            builder.Services.AddDbContext<CatsContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });


            builder.Services.AddScoped<IRepository<BusinessLogicLayer.Entity.Cat>, CatRepository>();
            builder.Services.AddScoped<ICatFinder, CatFinder>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add services to the container.
            builder.Services.AddGrpc();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<CatsService>();
            
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}