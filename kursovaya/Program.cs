using Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var config = builder.Configuration;

// Database context
var connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Client");
});

// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Login";
        options.AccessDeniedPath = "/Login";
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CoreAdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "CoreAdmin"));
    options.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
});

builder.Services.AddAuthorization();

// Add services to the container
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
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

app.MapControllerRoute(
    name: "User",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
//using Data;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.EntityFrameworkCore;


//var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Login/Login";
//        options.AccessDeniedPath = "/Login/Forbidden/";
//    });
//builder.Services.AddRazorPages();
//var config = builder.Configuration;
//var connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("NO string");
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
//var app = builder.Build();
//app.UseRouting();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapRazorPages();
//});

//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//// Add services to the container.
////builder.Services.AddRazorPages();
////var config = builder.Configuration;
////var connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("NO string");
////builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseAuthentication();
//app.UseRouting();

//app.UseAuthorization();

//app.MapRazorPages();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");


//app.MapControllerRoute(
//        name: "User",
//        pattern: "{controller=User}/{action=Index}/{id?}");
//app.Run();
