using HMS.Application.Contracts.Presistence;
using HMS.Application.Contracts.Services;
using HMS.Application.Mapping;
using HMS.Application.Services;
using HMS.Infrastructure.Data;
using HMS.Infrastructure.Presistence;
using HMS.Middleware;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

namespace HMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Company524",
                    Version = "v1",
                    Description = "API for education"
                });
            });


            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            //DATABASE
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            //REPOSITORIES
            builder.Services.AddScoped<IGuestRepository, GuestRepository>();
            builder.Services.AddScoped<IHotelRepository, HotelRepository>();
            builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
            builder.Services.AddScoped<IReservationRoomRepository, ReservationRoomRepository>();


            //SERVICES
            builder.Services.AddScoped<IHotelService, HotelService>();

            //MAPSTER
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(MappingConfig).Assembly);

            builder.Services.AddSingleton(config);
            builder.Services.AddScoped<IMapper, ServiceMapper>();


            var app = builder.Build();



            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
