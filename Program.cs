using Microsoft.EntityFrameworkCore;
using BugTrackingSystem.Database;
using BugTrackingSystem.Models.Entities;
using Microsoft.AspNetCore.Identity;
using BugTrackingSystem.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using BugTrackingSystem.Repositories.ProjectRepository;
using BugTrackingSystem.Repositories;
using BugTrackingSystem.Repositories.PermissionRepository;
using BugTrackingSystem.Helpers;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                                                 ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

#if DEBUG
connectionString = builder.Configuration.GetConnectionString("DebugConnection")
                                                            ?? throw new InvalidOperationException("Connection string 'DebugConnection' not found.");
#endif

builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseNpgsql(connectionString), ServiceLifetime.Scoped);

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
                       .AddEntityFrameworkStores<ApplicationDBContext>()
                       .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/AccessDenied";
});

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IPersonalSpaceRepository, PersonalSpaceRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IBugRepository, BugRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddRazorPages();

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seed")
{
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

app.MapControllerRoute(
    name: "login",
    pattern: "login",
    defaults: new { controller = "Account", action = "Login" });

app.MapControllerRoute(
    name: "register",
    pattern: "register",
    defaults: new { controller = "Account", action = "Register" });

app.MapRazorPages();

app.Run();
