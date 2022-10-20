using BookingSystem.Data;
using BookingSystem.Data.Identity;
using BookingSystem.Entities;
using BookingSystem.Extensions;
using BookingSystem.Utils.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityService(builder.Configuration);
builder.Services.AddSwaggerDocumentation();
builder.Services.AddCors(cors =>
{
    cors.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var identityContext = services.GetRequiredService<IdentityContext>();

        await identityContext.Database.MigrateAsync();

        var bookingContext = services.GetRequiredService<BookingContext>();

        await bookingContext.Database.MigrateAsync();

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            var admin = new IdentityRole("Admin");
            await roleManager.CreateAsync(admin);
        }
        if (!await roleManager.RoleExistsAsync("User"))
        {
            var user = new IdentityRole("User");
            await roleManager.CreateAsync(user);
        }
        var userManager = services.GetRequiredService<UserManager<User>>();
        await IdentityContextSeed.SeedUserAsync(userManager);

    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error ocurred during migrations!");
    }
};


app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
