
using DSS2_Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Enter 'Bear <jwt>'"
                });
            });

            // Database Context Service
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("TodoDb")));

            // JSON Web Token Settings
            string issuer = builder.Configuration["Jwt:Issuer"]!;
            string audience = builder.Configuration["Jwt:Audience"]!;
            string key = builder.Configuration["Jwt:Key"]!;

            builder.Services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
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

            // Query Parameter Service
            builder.Services.AddScoped<IQueryParamService, QueryParamService>();
            
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
            //dbContext.Database.Migrate();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
