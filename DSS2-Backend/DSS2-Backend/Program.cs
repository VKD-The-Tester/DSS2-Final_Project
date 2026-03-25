
using DSS2_Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace DSS2_Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Database Context Service
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("LocalDb")));
            // Password Service
            builder.Services.AddSingleton<IPasswordService, PasswordService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            /* These two lines of code will ensure that when a PostgreSQL container is active,
             * any changes/migrations will also be updated or tracked in the container. */
            var context = app.Services.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
