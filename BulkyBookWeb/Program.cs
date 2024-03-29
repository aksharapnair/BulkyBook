using BulkyBookWeb.DBContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = "Server=127.0.0.1;Port=3306;Database=bulkybooks;Uid=root;Pwd=Test1234;";
var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));

builder.Services.AddDbContext<ApplicationDBContext>(dbContextOptions => dbContextOptions
              .UseMySql(connectionString, serverVersion)
              // The following three options help with debugging, but should
              // be changed or removed for production.
              .LogTo(Console.WriteLine, LogLevel.Information)
              .EnableSensitiveDataLogging()
              .EnableDetailedErrors());

builder.Services.AddRazorPages();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
