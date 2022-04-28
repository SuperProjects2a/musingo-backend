﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using musingo_backend.Data;

#nullable disable

namespace musingo_backend.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.1.22076.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("musingo_backend.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool?>("IsRead")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("is_read");

                    b.Property<DateTime?>("SendTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("send_time")
                        .HasDefaultValueSql("CAST( GETDATE() AS DateTime )");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("text");

                    b.Property<int?>("sender_id")
                        .HasColumnType("int");

                    b.Property<int?>("transaction_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("sender_id");

                    b.HasIndex("transaction_id");

                    b.ToTable("messages", (string)null);
                });

            modelBuilder.Entity("musingo_backend.Models.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Cost")
                        .HasColumnType("double precision")
                        .HasColumnName("cost");

                    b.Property<DateTime?>("CreateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("create_time")
                        .HasDefaultValueSql("CAST( GETDATE() AS DateTime )");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)")
                        .HasColumnName("description");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(MAX)")
                        .HasColumnName("image_url");

                    b.Property<string>("ItemCategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("item_category");

                    b.Property<int>("OfferStatus")
                        .HasColumnType("int")
                        .HasColumnName("offer_status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)")
                        .HasColumnName("title");

                    b.Property<int>("owner_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("owner_id");

                    b.ToTable("offers", (string)null);
                });

            modelBuilder.Entity("musingo_backend.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Cost")
                        .HasColumnType("float")
                        .HasColumnName("cost");

                    b.Property<byte[]>("LastUpdateTime")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp")
                        .HasColumnName("last_update_time");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<int?>("buyer_id")
                        .HasColumnType("int");

                    b.Property<int?>("offer_id")
                        .HasColumnType("int");

                    b.Property<int?>("seller_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("buyer_id");

                    b.HasIndex("offer_id");

                    b.HasIndex("seller_id");

                    b.ToTable("transactions", (string)null);
                });

            modelBuilder.Entity("musingo_backend.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Birth")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("email");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("HouseNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(MAX)")
                        .HasColumnName("image_url");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)")
                        .HasColumnName("password");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1)
                        .HasColumnName("role");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("WalletBalance")
                        .HasColumnType("float")
                        .HasColumnName("wallet_balance");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("musingo_backend.Models.UserComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CommentText")
                        .HasColumnType("nvarchar(MAX)")
                        .HasColumnName("comment_text");

                    b.Property<double>("Rating")
                        .HasColumnType("double precision")
                        .HasColumnName("rating");

                    b.Property<int?>("transaction_id")
                        .HasColumnType("int");

                    b.Property<int?>("user_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("transaction_id");

                    b.HasIndex("user_id");

                    b.ToTable("user_comments", (string)null);
                });

            modelBuilder.Entity("musingo_backend.Models.UserOfferWatch", b =>
                {
                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OfferId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserOfferWatch", (string)null);
                });

            modelBuilder.Entity("musingo_backend.Models.Message", b =>
                {
                    b.HasOne("musingo_backend.Models.User", "Sender")
                        .WithMany()
                        .HasForeignKey("sender_id");

                    b.HasOne("musingo_backend.Models.Transaction", "Transaction")
                        .WithMany()
                        .HasForeignKey("transaction_id");

                    b.Navigation("Sender");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("musingo_backend.Models.Offer", b =>
                {
                    b.HasOne("musingo_backend.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("owner_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("musingo_backend.Models.Transaction", b =>
                {
                    b.HasOne("musingo_backend.Models.User", "Buyer")
                        .WithMany()
                        .HasForeignKey("buyer_id");

                    b.HasOne("musingo_backend.Models.Offer", "Offer")
                        .WithMany()
                        .HasForeignKey("offer_id");

                    b.HasOne("musingo_backend.Models.User", "Seller")
                        .WithMany()
                        .HasForeignKey("seller_id");

                    b.Navigation("Buyer");

                    b.Navigation("Offer");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("musingo_backend.Models.UserComment", b =>
                {
                    b.HasOne("musingo_backend.Models.Transaction", "Transaction")
                        .WithMany()
                        .HasForeignKey("transaction_id");

                    b.HasOne("musingo_backend.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("user_id");

                    b.Navigation("Transaction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("musingo_backend.Models.UserOfferWatch", b =>
                {
                    b.HasOne("musingo_backend.Models.Offer", "Offer")
                        .WithMany("UserOfferWatches")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("musingo_backend.Models.User", "User")
                        .WithMany("UserOfferWatches")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Offer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("musingo_backend.Models.Offer", b =>
                {
                    b.Navigation("UserOfferWatches");
                });

            modelBuilder.Entity("musingo_backend.Models.User", b =>
                {
                    b.Navigation("UserOfferWatches");
                });
#pragma warning restore 612, 618
        }
    }
}
