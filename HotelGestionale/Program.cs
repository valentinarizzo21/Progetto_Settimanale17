using EcommerceLiveEfCore.Services;
using HotelGestionale.Data;
using HotelGestionale.Models;
using HotelGestionale.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.SignIn.RequireConfirmedAccount =
        builder.Configuration.GetSection("Identity").GetValue<bool>("RequireConfirmedAccount");

    options.Password.RequiredLength =
        builder.Configuration.GetSection("Identity").GetValue<int>("RequiredLength");

    options.Password.RequireDigit =
        builder.Configuration.GetSection("Identity").GetValue<bool>("RequireDigit");

    options.Password.RequireLowercase =
        builder.Configuration.GetSection("Identity").GetValue<bool>("RequireLowercase");

    options.Password.RequireNonAlphanumeric =
        builder.Configuration.GetSection("Identity").GetValue<bool>("RequireNonAlphanumeric");

    options.Password.RequireUppercase =
        builder.Configuration.GetSection("Identity").GetValue<bool>("RequireUppercase");
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<LoggerService>();
builder.Services.AddScoped<UserManager<ApplicationUser>>(); 
builder.Services.AddScoped<SignInManager<ApplicationUser>>(); 
builder.Services.AddScoped<RoleManager<ApplicationRole>>(); 

LoggerService.ConfigureLogger();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
