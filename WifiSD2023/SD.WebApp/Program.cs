using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SD.Application.Extensions;
using SD.Application.Movies;
using SD.Persistence.Extensions;
using SD.Persistence.Repositories.DBContext;
using SD.WebApp.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("UserDbContext");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

var connectionStringMovie = builder.Configuration.GetConnectionString("MovieDbContext");
builder.Services.AddDbContext<MovieDbContext>(options =>
    options.UseSqlServer(connectionStringMovie));

builder.Services.RegisterApplicationServices();
builder.Services.RegisterRepositories();

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(MovieQueryHandler).GetTypeInfo().Assembly));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
