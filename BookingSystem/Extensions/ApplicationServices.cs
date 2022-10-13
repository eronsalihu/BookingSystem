using BookingSystem.Data;
using BookingSystem.Data.Identity;
using BookingSystem.Interfaces;
using BookingSystem.Services;
using BookingSystem.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Extensions
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookingContext>(options =>
            {
                options.UseSqlServer(connectionString: configuration.GetConnectionString("SqlConnection"));
            });
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(connectionString: configuration.GetConnectionString("SqlConnection"));
            });
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IGuestHouseService, GuestHouseService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IBookingService, BookingService>();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage.ToString()).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}
