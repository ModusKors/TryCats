using BusinessLogicLayer.Repository;
using BusinessLogicLayer.Service;
using DataAccessLayer;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace TryCats6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            string con = "Data Source=Cats.db";
            // устанавливаем контекст данных
            builder.Services.AddDbContext<CatsContext>(options => options.UseSqlite(con));
            builder.Services.AddScoped<ICatService, CatService>();

            builder.Services.AddControllers();
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
        }
    }
}