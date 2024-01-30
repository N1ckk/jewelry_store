using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WebApplication3.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connection = "server=localhost;user=root;password=39nIkItA93;database=jewelry_store;";
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 31))));
builder.Services.AddScoped<AuthenticationService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
     .AddCookie(options =>
     {
         options.Cookie.HttpOnly = true;
         options.ExpireTimeSpan = TimeSpan.FromMinutes(3); // Установите время жизни куки
         options.LoginPath = "/OrderManagement/Login"; // Путь к странице входа
         options.AccessDeniedPath = "/Account/AccessDenied"; // Путь к странице доступа запрещен
                                                             // Другие настройки аутентификации
     });



builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Установите время жизни сессии
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Catalog}/{id?}");

app.Run();
