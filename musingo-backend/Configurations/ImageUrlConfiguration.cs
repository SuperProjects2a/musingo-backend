using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using musingo_backend.Models;

namespace musingo_backend.Configurations;

public class ImageUrlConfiguration : IEntityTypeConfiguration<ImageUrl>
{
    public void Configure(EntityTypeBuilder<ImageUrl> builder)
    {
        builder.ToTable("image_urls")
            .HasOne(x => x.Offer)
            .WithMany()
            .HasForeignKey("offer_id");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Url)
            .HasColumnName("url")
            .HasColumnType("nvarchar(MAX)")
            .IsRequired();
    }
}