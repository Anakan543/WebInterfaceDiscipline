
using Laborotorna7.Service;
using Microsoft.OpenApi.Models;

namespace Laborotorna7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<IStoreProdictService, StoreProductsService>();//додавання інтерфейсу та сервісу для StoreProducts
            builder.Services.AddScoped<IUserService, UserService>(); 
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => { 
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "StoreProduct/UserControllers",
                    Version = "v1",
                    Description = "API для взаємодії з товарами й юзерами з магазину",
                    Contact = new OpenApiContact
                    {
                        Name = "Ivan",
                        Email = "kogutivan685@gmail.com"
                    }
                });
            });

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
