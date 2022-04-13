using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using musingo_backend.Models;

namespace musingo_backend.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions")
            .HasOne(x => x.Buyer)
            .WithMany()
            .HasForeignKey("buyer_id");
        
        builder.ToTable("transactions")
            .HasOne(x => x.Seller)
            .WithMany()
            .HasForeignKey("seller_id");
        
        builder.ToTable("transactions")
            .HasOne(x => x.Offer)
            .WithMany()
            .HasForeignKey("offer_id");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(x => x.LastUpdateTime)
            .HasColumnName("last_update_time")
            .HasColumnType("timestamp")
            .ValueGeneratedOnAddOrUpdate();

    }
}