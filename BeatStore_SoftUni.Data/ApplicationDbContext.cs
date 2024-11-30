using BeatStore_SoftUni.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BeatStore_SoftUni.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public virtual DbSet<Beat> Beats { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<BeatPlaylist> BeatsPlaylists { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<BeatGenre> BeatsGenres { get; set; }
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;
    }
}