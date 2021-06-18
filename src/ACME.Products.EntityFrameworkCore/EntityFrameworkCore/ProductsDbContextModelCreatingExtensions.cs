using ACME.Products.Categories;
using ACME.Products.ProductCategories;
using ACME.Products.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ACME.Products.EntityFrameworkCore
{
    public static class ProductsDbContextModelCreatingExtensions
    {
        public static void ConfigureProducts(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            builder.Entity<Product>(b =>
            {
                b.Property(a => a.Id).HasColumnName("ProductId");
                b.Property(a => a.ProductName).IsRequired().HasColumnType($"nvarchar({Products.ProductsConsts.MaxNameLength})");
                b.Property(a => a.Price).IsRequired().HasColumnType("smallmoney");
                b.Property(a => a.UnitsInStock).IsRequired().HasColumnType("smallint");
                b.Property(a => a.UnitsInStock).IsRequired().HasColumnType("smallint");
                b.HasMany<ProductCategory>(a=>a.ProductCategories)
                .WithOne()
                .HasForeignKey(a => a.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
                // un-explicity descriped in document
                // b.HasOne<Category>().WithMany().HasForeignKey(a => a.CategoryId);
                b.ToTable(ProductsConsts.DbTablePrefix + "Product", ProductsConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
            });


            builder.Entity<Category>(b =>
            {
                b.Property(a => a.Id).HasColumnName("CategoryId");
                b.Property(a => a.CategoryName).IsRequired().HasColumnType($"nvarchar({CategoirsConsts.MaxNameLength})");
                b.Property(a => a.ParentCategoryId).HasDefaultValue(0);
                b.HasMany<ProductCategory>()
                .WithOne(a=>a.Category)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
                b.ToTable(ProductsConsts.DbTablePrefix + "Category", ProductsConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
            });

            builder.Entity<ProductCategory>(b =>
            {
                b.HasKey(a=> new {a.ProductId,a.CategoryId});
                b.ToTable(ProductsConsts.DbTablePrefix + "ProductCategories", ProductsConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
            });
        }


    }
}