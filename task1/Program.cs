using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ApplicationDbContext") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

var services = app.Services;

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
       name: "default",
       pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "patients",
    pattern: "patients/{action=Index}/{id?}",
    defaults: new { controller = "Patients" });

app.MapControllerRoute(
    name: "visits",
    pattern: "visits/{action=Index}/{id?}",
    defaults: new { controller = "Visits" });

app.MapControllerRoute(
    name: "search",
    pattern: "Patients/Search",
    defaults: new { controller = "Patients", action = "Search" }
);

app.Run();


