using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Student_Management_App_MVC.Configurations;
using Student_Management_App_MVC.Middlewares;
using Student_Management_App_MVC.Services.Implementations;
using Student_Management_App_MVC.Services.Interfaces;
using StackExchange.Redis;
using Student_Management_App_MVC.Configurations;
using Student_Management_App_MVC.DataBase.Repositories.Implementations;
using Student_Management_App_MVC.DataBase.Repositories.Interfaces;
using Student_Management_App_MVC.DataBase;
using Student_Management_App_MVC.Middlewares;
using Student_Management_App_MVC.Services.Implementations;
using Student_Management_App_MVC.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "StudentManagementApp_";
});

// Add Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
        options.Cookie = new CookieBuilder
        {
            HttpOnly = true,
            Name = "StudentManagement.Auth",
            SameSite = SameSiteMode.Strict,
            SecurePolicy = CookieSecurePolicy.Always
        };
        options.LogoutPath = "/Account/Login"; // Redirect to login after logout
    });

// Register services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddSingleton<ICacheService, CacheService>();

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add global exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

app.Run();