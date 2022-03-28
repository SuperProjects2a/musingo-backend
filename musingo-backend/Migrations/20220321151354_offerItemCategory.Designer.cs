﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using musingo_backend.Data;

#nullable disable

namespace musingo_backend.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20220321151354_offerItemCategory")]
    partial class offerItemCategory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.1.22076.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

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

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)")
                        .HasColumnName("description");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(MAX)")
                        .HasColumnName("image_url");

                    b.Property<int>("ItemCategory")
                        .HasColumnType("int");

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

                    b.Property<byte[]>("LastUpdateTime")
                        .IsRequired()
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

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("email");

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

                    b.HasKey("Id");

                    b.HasIndex("transaction_id");

                    b.ToTable("user_comments", (string)null);
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

                    b.Navigation("Transaction");
                });
#pragma warning restore 612, 618
        }
    }
}