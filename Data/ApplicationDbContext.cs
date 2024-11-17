using BeatStore_SoftUni.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.Id)
                .HasConversion<Guid>();
            //
            modelBuilder.Entity<BeatPlaylist>()
                .HasKey(bp => new { bp.BeatId, bp.PlaylistId });

            modelBuilder.Entity<BeatPlaylist>()
                .HasOne(bp => bp.Beat)
                .WithMany(b => b.BeatPlaylists)
                .HasForeignKey(bp => bp.BeatId);

            modelBuilder.Entity<BeatPlaylist>()
                .HasOne(bp => bp.Playlist)
                .WithMany(p => p.BeatPlaylists)
                .HasForeignKey(bp => bp.PlaylistId);

            modelBuilder.Entity<BeatPlaylist>()
                .ToTable("BeatsPlaylists");
        }
        
        public virtual DbSet<Beat> Beats { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<BeatPlaylist> BeatsPlaylists { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
    }
}