using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using musingo_backend.Models;

namespace musingo_backend.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasColumnType("nvarchar(MAX)")
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnName("email")
            .HasMaxLength(100)
            .IsRequired();
        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(x => x.Password)
            .HasColumnName("password")
            .HasColumnType("nvarchar(MAX)")
            .IsRequired();

        builder.Property(x => x.ImageUrl)
            .HasColumnName("image_url")
            .HasColumnType("nvarchar(MAX)")
            .IsRequired(false);

        builder
            .HasMany(u => u.WatchedOffers)
            .WithMany(o => o.Watchers)
            .UsingEntity<UserOfferWatch>(
                x => 
                    x.HasOne(y => y.Offer)
                        .WithMany(y => y.UserOfferWatches)
                        .HasForeignKey(y => y.OfferId)
                        .OnDelete(DeleteBehavior.NoAction),
                x => 
                    x.HasOne(y => y.User)
                        .WithMany(y => y.UserOfferWatches)
                        .HasForeignKey(y => y.UserId)
                        .OnDelete(DeleteBehavior.NoAction)
                );

    }
}