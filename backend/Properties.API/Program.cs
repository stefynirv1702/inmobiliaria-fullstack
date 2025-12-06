using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Properties.Application.Services;
using Properties.Domain.Interfaces;
using Properties.Infraestructure.Data;
using Properties.Infraestructure.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // ⭐ 1. SQL SERVER CONFIGURATION
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        // ⭐ 2. INJECTION
        builder.Services.AddScoped(typeof(IRepository<,>), typeof(SqlRepository<,>));
        builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
        builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
        builder.Services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();
        builder.Services.AddScoped<IPropertyTraceRepository, PropertyTraceRepository>();

        // ⭐ 3. DOMAIN SERVICES
        builder.Services.AddScoped<PropertyService>();
        builder.Services.AddScoped<OwnerService>();
        builder.Services.AddScoped<TraceService>();

        // ⭐ 4. CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend",
                policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
        });

        // ⭐ 5. API AND SWAGGER
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Properties Company API",
                Version = "v1",
            });
        });

        var app = builder.Build();

        // ⭐ 6. MIDDLEWARE
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Properties Company API v1");
                c.RoutePrefix = "swagger";
            });
        }

        app.UseCors("AllowFrontend");

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}