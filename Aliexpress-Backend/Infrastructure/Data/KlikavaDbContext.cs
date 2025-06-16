using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Data
{
    public class KlikavaDbContext : DbContext
    {
        public KlikavaDbContext(DbContextOptions<KlikavaDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Notifications> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Password_hash).IsRequired();
                entity.Property(e => e.Address).IsRequired();
            });

            // Configure Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Price).HasPrecision(18, 2);
                entity.Property(e => e.Discount).HasPrecision(18, 2);

                // Configure the relationship with Seller (User)
                entity.HasOne(p => p.Seller)
                      .WithMany(u => u.Products)
                      .HasForeignKey(p => p.SellerId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Configure the relationship with Category
                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId);

                // Configure ImageUrls as a JSON column
                entity.Property(e => e.ImageUrls)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(),
                    new ValueComparer<List<string>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList())
    );
            });

            // Configure Category entity
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();

                // Self-referencing relationship for parent-child categories
                entity.HasOne(e => e.ParentCategory)
                      .WithMany(e => e.ChildCategories)
                      .HasForeignKey(e => e.ParentCategoryId)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Order entity
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TotalPrice).HasPrecision(18, 2);

                // Configure relationship with Buyer
                entity.HasOne(o => o.Buyer)
                      .WithMany(u => u.Orders)
                      .HasForeignKey(o => o.BuyerId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Configure relationship with Product
                entity.HasOne(o => o.Product)
                      .WithMany(p => p.Order)
                      .HasForeignKey(o => o.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure OrderItem entity (renamed from CategoryItem)
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PricePerItem).HasPrecision(18, 2);
                entity.Property(e => e.Subtotal).HasPrecision(18, 2);

                // Configure relationship with Order
                entity.HasOne(oi => oi.Order)
                      .WithMany(o => o.OrderItems)  // Note: This needs to be renamed in the Order class
                      .HasForeignKey(oi => oi.OrderID)
                      .OnDelete(DeleteBehavior.Cascade);

                // Configure relationship with Product
                entity.HasOne(oi => oi.Product)
                      .WithMany(p => p.OrderItem)  // Note: This needs to be renamed in the Product class
                      .HasForeignKey(oi => oi.ProductID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Payment entity
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Configure relationship with Order
                entity.HasOne(p => p.Order)
                      .WithMany(o => o.Payments)
                      .HasForeignKey(p => p.OrderID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Review entity
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Rating).HasPrecision(3, 1);

                // Configure relationship with Buyer
                entity.HasOne(r => r.Buyer)
                      .WithMany(u => u.Reviews)
                      .HasForeignKey(r => r.BuyerID)
                      .OnDelete(DeleteBehavior.Restrict);

                // Configure relationship with Product
                entity.HasOne(r => r.Product)
                      .WithMany(p => p.Reviews)
                      .HasForeignKey(r => r.ProductID)
                      .OnDelete(DeleteBehavior.Cascade);

                // Configure relationship with Seller
                entity.HasOne(r => r.Seller)
                      .WithMany() // No reverse navigation property needed
                      .HasForeignKey(r => r.SellerID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Chat entity
            modelBuilder.Entity<Chat>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Configure relationship with Buyer
                entity.HasOne(c => c.Buyer)
                      .WithMany(u => u.Chats)
                      .HasForeignKey(c => c.BuyerID)
                      .OnDelete(DeleteBehavior.Restrict);

                // Configure relationship with Seller
                entity.HasOne(c => c.Seller)
                      .WithMany() // No reverse navigation property needed
                      .HasForeignKey(c => c.SellerID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Messages entity
            modelBuilder.Entity<Messages>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Message).IsRequired();

                // Configure relationship with Chat
                entity.HasOne(m => m.Chat)
                      .WithMany(c => c.Messages)
                      .HasForeignKey(m => m.ChatID)
                      .OnDelete(DeleteBehavior.Cascade);

                // Note: You should add a relationship with the Sender (User)
                // This is missing in your current model
            });

            // Configure Notifications entity
            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Message).IsRequired();

                // Configure relationship with User (recipient)
                entity.HasOne(n => n.Recepient)
                      .WithMany(u => u.Notifications)
                      .HasForeignKey(n => n.RecepientID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
