
using DSS2_Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            // JSON Web Token Settings
            IConfigurationSection jsonWebToken = builder.Configuration.GetSection("Jwt");
            string issuer = jsonWebToken["Issuer"]!;
            string audience = jsonWebToken["Audience"]!;
            string key = jsonWebToken["Key"]!;

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                        ClockSkew = TimeSpan.FromMinutes(1)
                    };
                });
            
            builder.Services.AddAuthorization();

            // Authorization Services
            builder.Services.AddSingleton<IPasswordService, PasswordService>();
            builder.Services.AddSingleton<ITokenService, TokenService>();

            // Reading claims in services
            builder.Services.AddHttpContextAccessor();

            // Lowercase URLS
            builder.Services.AddRouting(options => options.LowercaseUrls = true);
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            /* These two lines of code will ensure that when a PostgreSQL container is active,
             * any changes/migrations will also be updated or tracked in the container. */
            //var dbContext = app.Services.GetRequiredService<ApplicationDbContext>();
            //context.Database.Migrate();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
