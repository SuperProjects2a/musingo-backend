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
        
        builder.Property(x => x.ImageUrl)
            .HasColumnName("image_url")
            .HasColumnType("nvarchar(MAX)")
            .IsRequired(false);
        
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
    }
}