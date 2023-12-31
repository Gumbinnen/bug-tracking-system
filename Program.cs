using Microsoft.EntityFrameworkCore;
using BugTrackingSystem.Database;
using BugTrackingSystem.Models.Entities;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                                                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

#if DEBUG
connectionString = builder.Configuration.GetConnectionString("DebugConnection") ?? throw new InvalidOperationException("Connection string 'DebugConnection' not found.");
#endif

builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseNpgsql(connectionString), ServiceLifetime.Scoped);

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
                       .AddEntityFrameworkStores<ApplicationDBContext>()
                       .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seed")
{
    Seeder.SeedData(app);
    await Seeder.SeedDataAsync(app);
}

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
app.MapRazorPages();

app.Run();
