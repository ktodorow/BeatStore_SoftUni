using BeatStore_SoftUni.Data;
using BeatStore_SoftUni.Data.Models;
using BeatStore_SoftUni.Data.Repository.Interfaces;
using BeatStore_SoftUni.Data.Repository;
using BeatStore_SoftUni.Services.Data;
using BeatStore_SoftUni.Services.Data.Interfaces;
using static BeatStore_SoftUni.ProgramExtensions;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddRazorPages();
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Disable email confirmation for accounts
    options.User.RequireUniqueEmail = true;       
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));
builder.Services.AddScoped<IBeatService, BeatService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddControllersWithViews();
 
WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Errors/500");
    app.UseStatusCodePagesWithReExecute("/Errors/{0}");
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

app.MapFallbackToController("NotFoundPage", "Errors");

app.MapRazorPages();

app.MigrateDatabase<ApplicationDbContext>();
await SeedData.SeedDatabase(app.Services);

app.Run();