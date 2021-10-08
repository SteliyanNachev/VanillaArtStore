namespace VanillaArtStore.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using VanillaArtStore.Data.Models;

    public class VanillaArtDbContext : IdentityDbContext<User>
    {

        public VanillaArtDbContext(DbContextOptions<VanillaArtDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; init; }
        public DbSet<Category> Categories { get; init; }
        public DbSet<Review> Reviews { get; init; }
        public DbSet<Image> Images { get; init; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Product>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Review>()
                .HasOne(c => c.Product)
                .WithMany(c => c.Reviews)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Review>()
                .HasOne(c => c.User)
                .WithMany(c => c.Reviews)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder
                .Entity<Image>()
                .HasOne(c => c.Product)
                .WithMany(c => c.Images)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
