using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using musingo_backend.Models;

namespace musingo_backend.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("messages")
            .HasOne(x => x.Transaction)
            .WithMany()
            .IsRequired(false)
            .HasForeignKey("transaction_id");

        builder.ToTable("messages")
            .HasOne(x => x.Sender)
            .WithMany()
            .IsRequired(false)
            .HasForeignKey("sender_id");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Text)
            .HasColumnName("text")
            .HasColumnType("nvarchar(max)")
            .IsRequired();

        builder.Property(x=>x.SendTime)
            .HasColumnName("send_time")
            .HasColumnType("datetime")
            .HasDefaultValueSql("CAST( GETDATE() AS DateTime )");

        builder.Property(x => x.IsRead)
            .HasColumnName("is_read")
            .HasColumnType("bit")
            .HasDefaultValue(false);
    }
}