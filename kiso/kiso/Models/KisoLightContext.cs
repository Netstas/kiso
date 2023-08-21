using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace kiso.Models;

public partial class KisoLightContext : DbContext
{
    public KisoLightContext()
    {
    }

    public KisoLightContext(DbContextOptions<KisoLightContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<ArticleCategory> ArticleCategories { get; set; }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<ConfigSite> ConfigSites { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Recruitment> Recruitments { get; set; }

    public virtual DbSet<Subcribe> Subcribes { get; set; }

    public virtual DbSet<Ward> Wards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-5T3B93H;Database=KisoLight;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Admins");

            entity.Property(e => e.Password).HasMaxLength(60);
        });

        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Articles");

            entity.HasIndex(e => e.ArticleCategoryId, "IX_ArticleCategoryId");

            entity.HasIndex(e => e.RecruitmentId, "IX_Recruitment_Id");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.DescriptionMeta).HasMaxLength(500);
            entity.Property(e => e.RecruitmentId).HasColumnName("Recruitment_Id");
            entity.Property(e => e.Subject).HasMaxLength(150);
            entity.Property(e => e.TitleMeta).HasMaxLength(100);
            entity.Property(e => e.Url).HasMaxLength(300);

            entity.HasOne(d => d.ArticleCategory).WithMany(p => p.Articles)
                .HasForeignKey(d => d.ArticleCategoryId)
                .HasConstraintName("FK_dbo.Articles_dbo.ArticleCategories_ArticleCategoryId");

            entity.HasOne(d => d.Recruitment).WithMany(p => p.Articles)
                .HasForeignKey(d => d.RecruitmentId)
                .HasConstraintName("FK_dbo.Articles_dbo.Recruitments_Recruitment_Id");
        });

        modelBuilder.Entity<ArticleCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.ArticleCategories");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.DescriptionMeta).HasMaxLength(500);
            entity.Property(e => e.Image).HasMaxLength(500);
            entity.Property(e => e.TitleMeta).HasMaxLength(100);
            entity.Property(e => e.Url).HasMaxLength(500);
        });

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Banners");

            entity.Property(e => e.BannerName).HasMaxLength(100);
            entity.Property(e => e.Image).HasMaxLength(500);
            entity.Property(e => e.Slogan).HasMaxLength(500);
            entity.Property(e => e.Url).HasMaxLength(500);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK_dbo.Carts");

            entity.HasIndex(e => e.ProductId, "IX_ProductId");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_dbo.Carts_dbo.Products_ProductId");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Cities");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Prefix).HasMaxLength(20);
        });

        modelBuilder.Entity<ConfigSite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.ConfigSites");

            entity.Property(e => e.AboutUrl).HasMaxLength(500);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Facebook).HasMaxLength(500);
            entity.Property(e => e.GoogleAnalytics).HasMaxLength(4000);
            entity.Property(e => e.GoogleMap).HasMaxLength(4000);
            entity.Property(e => e.GooglePlus).HasMaxLength(500);
            entity.Property(e => e.Hethong).HasMaxLength(500);
            entity.Property(e => e.Hotline).HasMaxLength(50);
            entity.Property(e => e.Instagram).HasMaxLength(500);
            entity.Property(e => e.LinkMap).HasMaxLength(500);
            entity.Property(e => e.LiveChat).HasMaxLength(4000);
            entity.Property(e => e.NameBank).HasMaxLength(200);
            entity.Property(e => e.NumberBank).HasMaxLength(200);
            entity.Property(e => e.Pinterest).HasMaxLength(500);
            entity.Property(e => e.Skype).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.Twitter).HasMaxLength(500);
            entity.Property(e => e.UseBank).HasMaxLength(200);
            entity.Property(e => e.Youtube).HasMaxLength(500);
            entity.Property(e => e.Zalo).HasMaxLength(500);
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Contacts");

            entity.Property(e => e.Body).HasMaxLength(4000);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Mobile).HasMaxLength(10);
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Districts");

            entity.HasIndex(e => e.CityId, "IX_CityId");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Prefix).HasMaxLength(20);

            entity.HasOne(d => d.City).WithMany(p => p.Districts)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_dbo.Districts_dbo.Cities_CityId");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Feedbacks");

            entity.Property(e => e.Classify).HasMaxLength(100);
            entity.Property(e => e.Content).HasMaxLength(700);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Materials");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasMany(d => d.Products).WithMany(p => p.Materials)
                .UsingEntity<Dictionary<string, object>>(
                    "MaterialProduct",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_dbo.MaterialProducts_dbo.Products_Product_Id"),
                    l => l.HasOne<Material>().WithMany()
                        .HasForeignKey("MaterialId")
                        .HasConstraintName("FK_dbo.MaterialProducts_dbo.Materials_Material_Id"),
                    j =>
                    {
                        j.HasKey("MaterialId", "ProductId").HasName("PK_dbo.MaterialProducts");
                        j.ToTable("MaterialProducts");
                        j.HasIndex(new[] { "MaterialId" }, "IX_Material_Id");
                        j.HasIndex(new[] { "ProductId" }, "IX_Product_Id");
                        j.IndexerProperty<int>("MaterialId").HasColumnName("Material_Id");
                        j.IndexerProperty<int>("ProductId").HasColumnName("Product_Id");
                    });
        });

        modelBuilder.Entity<MigrationHistory>(entity =>
        {
            entity.HasKey(e => new { e.MigrationId, e.ContextKey }).HasName("PK_dbo.__MigrationHistory");

            entity.ToTable("__MigrationHistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ContextKey).HasMaxLength(300);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Orders");

            entity.HasIndex(e => e.CityId, "IX_CityId");

            entity.HasIndex(e => e.DistrictId, "IX_DistrictId");

            entity.HasIndex(e => e.WardId, "IX_WardId");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerInfoAddress)
                .HasMaxLength(200)
                .HasColumnName("CustomerInfo_Address");
            entity.Property(e => e.CustomerInfoBody)
                .HasMaxLength(200)
                .HasColumnName("CustomerInfo_Body");
            entity.Property(e => e.CustomerInfoEmail)
                .HasMaxLength(50)
                .HasColumnName("CustomerInfo_Email");
            entity.Property(e => e.CustomerInfoFullname)
                .HasMaxLength(50)
                .HasColumnName("CustomerInfo_Fullname");
            entity.Property(e => e.CustomerInfoIsNewMember).HasColumnName("CustomerInfo_IsNewMember");
            entity.Property(e => e.CustomerInfoMobile)
                .HasMaxLength(11)
                .HasColumnName("CustomerInfo_Mobile");
            entity.Property(e => e.CustomerInfoOrther)
                .HasMaxLength(200)
                .HasColumnName("CustomerInfo_Orther");
            entity.Property(e => e.MaDonHang).HasMaxLength(50);
            entity.Property(e => e.TransportDate).HasColumnType("datetime");

            entity.HasOne(d => d.City).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_dbo.Orders_dbo.Cities_CityId");

            entity.HasOne(d => d.District).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK_dbo.Orders_dbo.Districts_DistrictId");

            entity.HasOne(d => d.Ward).WithMany(p => p.Orders)
                .HasForeignKey(d => d.WardId)
                .HasConstraintName("FK_dbo.Orders_dbo.Wards_WardId");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.OrderDetails");

            entity.HasIndex(e => e.OrderId, "IX_OrderId");

            entity.HasIndex(e => e.ProductId, "IX_ProductId");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_dbo.OrderDetails_dbo.Orders_OrderId");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_dbo.OrderDetails_dbo.Products_ProductId");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Products");

            entity.HasIndex(e => e.ProductCategoryId, "IX_ProductCategoryId");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DescriptionMeta).HasMaxLength(500);
            entity.Property(e => e.MaSp)
                .HasMaxLength(50)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PriceSale).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TitleMeta).HasMaxLength(100);
            entity.Property(e => e.Url).HasMaxLength(300);

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategoryId)
                .HasConstraintName("FK_dbo.Products_dbo.ProductCategories_ProductCategoryId");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.ProductCategories");

            entity.Property(e => e.CategoryName).HasMaxLength(80);
            entity.Property(e => e.CoverImage).HasMaxLength(500);
            entity.Property(e => e.DescriptionMeta).HasMaxLength(500);
            entity.Property(e => e.Image).HasMaxLength(500);
            entity.Property(e => e.TitleIntroduce).HasMaxLength(500);
            entity.Property(e => e.TitleMeta).HasMaxLength(100);
            entity.Property(e => e.Url).HasMaxLength(500);

            entity.HasMany(d => d.Materials).WithMany(p => p.ProductCategories)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductCategoryMaterial",
                    r => r.HasOne<Material>().WithMany()
                        .HasForeignKey("MaterialId")
                        .HasConstraintName("FK_dbo.ProductCategoryMaterials_dbo.Materials_Material_Id"),
                    l => l.HasOne<ProductCategory>().WithMany()
                        .HasForeignKey("ProductCategoryId")
                        .HasConstraintName("FK_dbo.ProductCategoryMaterials_dbo.ProductCategories_ProductCategory_Id"),
                    j =>
                    {
                        j.HasKey("ProductCategoryId", "MaterialId").HasName("PK_dbo.ProductCategoryMaterials");
                        j.ToTable("ProductCategoryMaterials");
                        j.HasIndex(new[] { "MaterialId" }, "IX_Material_Id");
                        j.HasIndex(new[] { "ProductCategoryId" }, "IX_ProductCategory_Id");
                        j.IndexerProperty<int>("ProductCategoryId").HasColumnName("ProductCategory_Id");
                        j.IndexerProperty<int>("MaterialId").HasColumnName("Material_Id");
                    });
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Questions");
        });

        modelBuilder.Entity<Recruitment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Recruitments");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.InfoAddress)
                .HasMaxLength(200)
                .HasColumnName("Info_Address");
            entity.Property(e => e.InfoBody)
                .HasMaxLength(200)
                .HasColumnName("Info_Body");
            entity.Property(e => e.InfoEmail)
                .HasMaxLength(50)
                .HasColumnName("Info_Email");
            entity.Property(e => e.InfoFullName)
                .HasMaxLength(50)
                .HasColumnName("Info_FullName");
            entity.Property(e => e.InfoMobile)
                .HasMaxLength(11)
                .HasColumnName("Info_Mobile");
        });

        modelBuilder.Entity<Subcribe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Subcribes");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
        });

        modelBuilder.Entity<Ward>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Wards");

            entity.HasIndex(e => e.DistrictId, "IX_DistrictId");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Prefix).HasMaxLength(20);

            entity.HasOne(d => d.District).WithMany(p => p.Wards)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK_dbo.Wards_dbo.Districts_DistrictId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
