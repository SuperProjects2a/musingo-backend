using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using musingo_backend.Models;

namespace musingo_backend.Configurations;

public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.ToTable("offers")
            .HasOne(x => x.Owner)
            .WithMany()
            .IsRequired()
            .HasForeignKey("owner_id");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Title)
            .HasColumnName("title")
            .HasColumnType("nvarchar(MAX)")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasColumnType("nvarchar(MAX)")
            .IsRequired();
        
        builder.Property(x => x.OfferStatus)
            .HasColumnName("offer_status")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(x => x.Cost)
            .HasColumnName("cost")
            .HasColumnType("double precision")
            .IsRequired();

        builder.Property(x => x.ItemCategory)
            .HasColumnName("item_category")
            .HasColumnType("nvarchar(30)")
            .IsRequired();

        builder.Property(x => x.CreateTime)
            .HasColumnName("create_time")
            .HasColumnType("datetime")
            .HasDefaultValueSql("CAST( GETDATE() AS DateTime )");

        builder.Property(x => x.IsBanned)
            .HasColumnName("is_banned")
            .HasColumnType("bit")
            .HasDefaultValue(false);

        builder.Property(x => x.Email)
            .HasColumnName("email")
            .HasColumnType("nvarchar(MAX)")
            .IsRequired();

        builder.Property(x => x.City)
            .HasColumnName("city")
            .HasColumnType("nvarchar(MAX)")
            .IsRequired();

        builder.Property(x => x.PhoneNumber)
            .HasColumnName("phone_number")
            .HasColumnType("nvarchar(MAX)")
            .IsRequired();
    }
}