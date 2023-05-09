using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SD.Application.Extensions;
using SD.Application.Movies;
using SD.Persistence.Extensions;
using SD.Persistence.Repositories.DBContext;
using SD.WebApp.Data;
using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using Globalization.Attributes;
using Globalization;
using System.Resources;

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


//Broweser Spracherkennung
builder.Services.Configure<RequestLocalizationOptions>(opts =>
{
    var supportedCulture = new List<CultureInfo> {
        new CultureInfo("en"),
        new CultureInfo("de")
    };

    opts.DefaultRequestCulture = new RequestCulture("de");
    opts.SupportedCultures = supportedCulture;
    opts.SupportedUICultures = supportedCulture;
});

// ASP.NET MVC für lokalisierte CSHTML-Dateien konfigurieren
builder.Services.AddMvc().AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix,
    opts => { opts.ResourcesPath = "Ressources"; });

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();



builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

//Browser-Spracherkennung aktivieren
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.UseSession();

LocalizedDescriptionAttribute.Setup(new ResourceManager(typeof(BasicRes)));

app.Run();
