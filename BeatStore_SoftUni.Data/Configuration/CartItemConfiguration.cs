using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BeatStore_SoftUni.Data.Models;

namespace BeatStore_SoftUni.Data.Configuration
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            // Primary key
            builder.HasKey(ci => ci.Id);

            // Relationships
            builder.HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ci => ci.Beat)
                .WithMany()
                .HasForeignKey(ci => ci.BeatId)
                .OnDelete(DeleteBehavior.Restrict);

            // Table name
            builder.ToTable("CartItems");
        }
    }
}
