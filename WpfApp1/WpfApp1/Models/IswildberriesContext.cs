﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using WpfApp1.Models.DTO;

namespace WpfApp1.Models
{
    public partial class ISWildberriesContext : DbContext
    {
        #if DEBUG
            public static readonly Microsoft.Extensions.Logging.LoggerFactory _myLoggerFactory =
                new LoggerFactory(new[] {
                    new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
                });
        #endif

        public ISWildberriesContext() { }

        public ISWildberriesContext(DbContextOptions<ISWildberriesContext> options)
            : base(options) { }

        public virtual DbSet<Cards> Cards { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<DeferredProducts> DeferredProducts { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<LoginedWorkerRights> LoginedWorkerRights { get; set; }
        public virtual DbSet<OrderHistory> OrderHistory { get; set; }
        public virtual DbSet<OrderHistoryDTO> OrderHistoryDTO { get; set; }
        public virtual DbSet<OrderStatuses> OrderStatuses { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<PriceHistory> PriceHistory { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<ProductsOnStorages> ProductsOnStorages { get; set; }
        public virtual DbSet<ProductsParameters> ProductsParameters { get; set; }
        public virtual DbSet<ReceiptOfProductsToStorages> ReceiptOfProductsToStorages { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public virtual DbSet<ReviewImage> ReviewImages { get; set; }
        public virtual DbSet<Rights> Rights { get; set; }
        public virtual DbSet<SectionRights> SectionRights { get; set; }
        public virtual DbSet<Sections> Sections { get; set; }
        public virtual DbSet<StorageTypes> StorageTypes { get; set; }
        public virtual DbSet<StorageWorkerShifts> StorageWorkerShifts { get; set; }
        public virtual DbSet<Storages> Storages { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<TableFiles> TableFiles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserAverageCostDTO> UserAverageCostDTO { get; set; }
        public virtual DbSet<Workers> Workers { get; set; }
        public virtual DbSet<WorkersInOrdersDTO> WorkersInOrdersDTO { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = LAPTOP-LJNAL6S6; Initial Catalog = ISWildberries; Integrated Security = True; Encrypt = False");

            #if DEBUG
                optionsBuilder.UseLoggerFactory(_myLoggerFactory);
            #endif
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cards>(entity =>
            {
                entity.HasKey(e => e.CardNumber);

                entity.ToTable("cards");

                entity.Property(e => e.CardNumber)
                    .HasMaxLength(255)
                    .HasColumnName("card_number");

                entity.Property(e => e.CardOwner)
                    .HasMaxLength(255)
                    .HasColumnName("card_owner");

                entity.Property(e => e.Cvc).HasColumnName("cvc");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Validity)
                    .HasMaxLength(255)
                    .HasColumnName("validity");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cards_users");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ParentCategoryId).HasColumnName("parent_category_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.HasOne(d => d.ParentCategory)
                    .WithMany(p => p.InverseParentCategory)
                    .HasForeignKey(d => d.ParentCategoryId)
                    .HasConstraintName("FK_categories_categories");
            });

            modelBuilder.Entity<Countries>(entity =>
            {
                entity.ToTable("countries");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<DeferredProducts>(entity =>
            {
                entity.ToTable("deferred_products");

                entity.HasIndex(e => new { e.UserId, e.ProductId }, "uc_user_id_product_id_in_def_products")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.DeferredProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_deferred_products_products");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DeferredProducts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_deferred_products_users");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__product___3213E83F60EBE42D");
                entity.ToTable("product_images");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.ProductImage1).HasColumnName("product_image");
                entity.HasOne(d => d.Product).WithMany(p => p.Images)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__product_i__produ__67DE6983");
            });

            modelBuilder.Entity<ReviewImage>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__review_i__3213E83F630EB357");
                entity.ToTable("review_images");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.ReviewImage1).HasColumnName("review_image");
                entity.HasOne(d => d.Review).WithMany(p => p.Images)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__review_im__order__6ABAD62E");
            });

            modelBuilder.Entity<OrderHistory>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.StatusChangedAt });

                entity.ToTable("order_history");

                entity.HasIndex(e => e.OrderId, "IX_order_history");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.StatusChangedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("status_changed_at");

                entity.Property(e => e.CurrentStorageId).HasColumnName("current_storage_id");

                entity.Property(e => e.IsLastStatus).HasColumnName("is_last_status");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.HasOne(d => d.CurrentStorage)
                    .WithMany(p => p.OrderHistory)
                    .HasForeignKey(d => d.CurrentStorageId)
                    .HasConstraintName("FK_order_history_storages");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderHistory)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_order_history_orders");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.OrderHistory)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_order_history_order_statuses");

                entity.HasMany(d => d.Worker)
                    .WithMany(p => p.OrderHistory)
                    .UsingEntity<Dictionary<string, object>>(
                        "WorkersInOrders",
                        l => l.HasOne<Workers>().WithMany().HasForeignKey("WorkerId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__workers_i__worke__656C112C"),
                        r => r.HasOne<OrderHistory>().WithMany().HasForeignKey("OrderId", "StatusChangedAt").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_workers_in_orders_order_history"),
                        j =>
                        {
                            j.HasKey("OrderId", "StatusChangedAt", "WorkerId");

                            j.ToTable("workers_in_orders");

                            j.IndexerProperty<int>("OrderId").HasColumnName("order_id");

                            j.IndexerProperty<DateTime>("StatusChangedAt").HasColumnType("datetime").HasColumnName("status_changed_at");

                            j.IndexerProperty<int>("WorkerId").HasColumnName("worker_id");
                        });
            });

            modelBuilder.Entity<OrderStatuses>(entity =>
            {
                entity.ToTable("order_statuses");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EstimatedDeliveryAt)
                    .HasColumnType("datetime")
                    .HasColumnName("estimated_delivery_at")
                    .HasDefaultValueSql("(dateadd(week,(2),getdate()))");

                entity.Property(e => e.PickUpPointId).HasColumnName("pick_up_point_id");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ProductCount).HasColumnName("product_count");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.PickUpPoint)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PickUpPointId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDERS_PICK_UP_POINT_ID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_orders_products");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_orders_users");
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.ToTable("posts");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<PriceHistory>(entity =>
            {
                entity.ToTable("price_history");

                entity.HasIndex(e => new { e.ProductId, e.PriceDate }, "u_price_history_product_id_price_date").IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Price).HasColumnName("price");
                entity.Property(e => e.PriceDate)
                    .HasColumnType("datetime")
                    .HasColumnName("price_date");
                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.HasOne(d => d.Product).WithMany(p => p.PriceHistory)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_price_history_products");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.ToTable("products");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

                entity.Property(e => e.SupplierPercent)
                    .HasColumnName("supplier_percent")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_products_categories1");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_products_suppliers");
            });

            modelBuilder.Entity<ProductsOnStorages>(entity =>
            {
                entity.HasKey(e => new { e.StorageId, e.ProductId })
                    .HasName("PK_pos_id");

                entity.ToTable("products_on_storages");

                entity.Property(e => e.StorageId).HasColumnName("storage_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ProductAmount).HasColumnName("product_amount");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductsOnStorages)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_products_on_storages_products");

                entity.HasOne(d => d.Storage)
                    .WithMany(p => p.ProductsOnStorages)
                    .HasForeignKey(d => d.StorageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_products_on_storages_storages");
            });

            modelBuilder.Entity<ProductsParameters>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.ParameterTitle })
                    .HasName("PK_pr_par_id");

                entity.ToTable("products_parameters");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ParameterTitle)
                    .HasMaxLength(255)
                    .HasColumnName("parameter_title");

                entity.Property(e => e.ParameterValue)
                    .HasMaxLength(255)
                    .HasColumnName("parameter_value");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductsParameters)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_products_parameters_products");
            });

            modelBuilder.Entity<ReceiptOfProductsToStorages>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.ToTable("receipt_of_products_to_storages");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.StorageId).HasColumnName("storage_id");

                entity.Property(e => e.ReceivedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("received_at");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ReceiptOfProductsToStorages)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_receipt_of_products_to_storages_products");

                entity.HasOne(d => d.Storage)
                    .WithMany(p => p.ReceiptOfProductsToStorages)
                    .HasForeignKey(d => d.StorageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__receipt_o__stora__7FEAFD3E");
            });

            modelBuilder.Entity<Reviews>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.ToTable("reviews");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("order_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.ReviewText)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("review_text");

                entity.Property(e => e.Stars).HasColumnName("stars");

                entity.HasOne(d => d.Order)
                    .WithOne(p => p.Reviews)
                    .HasForeignKey<Reviews>(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_reviews_orders");
            });

            modelBuilder.Entity<Rights>(entity =>
            {
                entity.ToTable("rights");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<SectionRights>(entity =>
            {
                entity.ToTable("section_rights");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.RightId).HasColumnName("right_id");

                entity.Property(e => e.SectionId).HasColumnName("section_id");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.SectionRights)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__section_r__post___0E6E26BF");

                entity.HasOne(d => d.Right)
                    .WithMany(p => p.SectionRights)
                    .HasForeignKey(d => d.RightId)
                    .HasConstraintName("FK__section_r__right__0D7A0286");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.SectionRights)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK__section_r__secti__0C85DE4D");
            });

            modelBuilder.Entity<Sections>(entity =>
            {
                entity.ToTable("sections");

                entity.HasIndex(e => e.SectionKey, "u_sections_section_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.SectionKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("section_key");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<StorageTypes>(entity =>
            {
                entity.ToTable("storage_types");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<StorageWorkerShifts>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.ToTable("storage_worker_shifts");

                entity.Property(e => e.WorkerId).HasColumnName("worker_id");

                entity.Property(e => e.StorageId).HasColumnName("storage_id");

                entity.Property(e => e.StartedShiftAt)
                    .HasColumnType("datetime")
                    .HasColumnName("started_shift_at");

                entity.Property(e => e.FinishedShiftAt)
                    .HasColumnType("datetime")
                    .HasColumnName("finished_shift_at");

                entity.HasOne(d => d.Storage)
                    .WithMany(p => p.StorageWorkerShifts)
                    .HasForeignKey(d => d.StorageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_workers_at_storages_storages");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.StorageWorkerShifts)
                    .HasForeignKey(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_workers_at_storages_workers");
            });

            modelBuilder.Entity<Storages>(entity =>
            {
                entity.ToTable("storages");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .HasColumnName("country");

                entity.Property(e => e.FederalSubject)
                    .HasMaxLength(255)
                    .HasColumnName("federal_subject");

                entity.Property(e => e.HouseNumber)
                    .HasMaxLength(255)
                    .HasColumnName("house_number");

                entity.Property(e => e.Locality)
                    .HasMaxLength(255)
                    .HasColumnName("locality");

                entity.Property(e => e.StorageType).HasColumnName("storage_type");

                entity.Property(e => e.Street)
                    .HasMaxLength(255)
                    .HasColumnName("street");

                entity.HasOne(d => d.StorageTypeNavigation)
                    .WithMany(p => p.Storages)
                    .HasForeignKey(d => d.StorageType)
                    .HasConstraintName("fk_storage_type");
            });

            modelBuilder.Entity<Suppliers>(entity =>
            {
                entity.ToTable("suppliers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Birthday)
                    .HasColumnType("datetime")
                    .HasColumnName("birthday");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("firstname");

                entity.Property(e => e.IsMale).HasColumnName("is_male");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("lastname");

                entity.Property(e => e.OrderCode).HasColumnName("order_code");

                entity.Property(e => e.Patronymic)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("patronymic");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("phone_number");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__users__country_i__245D67DE");
            });

            modelBuilder.Entity<Workers>(entity =>
            {
                entity.ToTable("workers");

                entity.HasIndex(e => e.WorkerLogin, "U_workers_login")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateOfBirthday)
                    .HasColumnType("datetime")
                    .HasColumnName("date_of_birthday");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(255)
                    .HasColumnName("firstname");

                entity.Property(e => e.IsMale).HasColumnName("is_male");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(255)
                    .HasColumnName("lastname");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(255)
                    .HasColumnName("patronymic");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .HasColumnName("phone_number");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.WorkerLogin)
                    .HasMaxLength(255)
                    .HasColumnName("worker_login");

                entity.Property(e => e.WorkerPassword).HasColumnName("worker_password");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Workers)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_workers_posts");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

