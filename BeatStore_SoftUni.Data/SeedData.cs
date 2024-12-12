using BeatStore_SoftUni.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BeatStore_SoftUni.Data
{
    public static class SeedData
    {
        public static async Task SeedDatabase(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

                context.Database.Migrate();

                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
                }

                if (!await roleManager.RoleExistsAsync("User"))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>("User"));
                }

                if (!userManager.Users.Any())
                {
                    var admin = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = "admin@example.com",
                        EmailConfirmed = true,
                        DateJoined = DateTime.UtcNow
                    };

                    await userManager.CreateAsync(admin, "Admin@1234");
                    await userManager.AddToRoleAsync(admin, "Admin");

                    var user = new ApplicationUser
                    {
                        UserName = "user",
                        Email = "user@example.com",
                        EmailConfirmed = true,
                        DateJoined = DateTime.UtcNow
                    };

                    await userManager.CreateAsync(user, "User@1234");
                    await userManager.AddToRoleAsync(user, "User");
                }

                if (!context.Beats.Any())
                {
                    var genres = context.Genres.Take(2).ToList();
                    var admin = await userManager.FindByNameAsync("admin");
                    var user = await userManager.FindByNameAsync("user");

                    context.Beats.AddRange(
                        new Beat
                        {
                            Id = Guid.NewGuid(),
                            Title = "Epic Soundtrack",
                            ArtistId = admin.Id,
                            Price = 9.99m,
                            AudioFileUrl = "https://codeskulptor-demos.commondatastorage.googleapis.com/GalaxyInvaders/theme_01.mp3",
                            CoverArtUrl = "https://funfactco.com/cdn/shop/articles/fun-facts-about-video-games.jpg?v=1699969164",
                            DateUploaded = DateTime.UtcNow,
                            IsActive = true
                        },
                        new Beat
                        {
                            Id = Guid.NewGuid(),
                            Title = "Chill Vibes",
                            ArtistId = user.Id,
                            Price = 4.99m,
                            AudioFileUrl = "https://commondatastorage.googleapis.com/codeskulptor-demos/DDR_assets/Kangaroo_MusiQue_-_The_Neverwritten_Role_Playing_Game.mp3",
                            CoverArtUrl = "https://store-images.s-microsoft.com/image/apps.808.14492077886571533.be42f4bd-887b-4430-8ed0-622341b4d2b0.c8274c53-105e-478b-9f4b-41b8088210a3?q=90&w=256&h=384&mode=crop&format=jpg&background=%23FFFFFF",
                            DateUploaded = DateTime.UtcNow,
                            IsActive = true
                        }
                    );

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
