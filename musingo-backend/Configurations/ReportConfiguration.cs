using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using musingo_backend.Models;

namespace musingo_backend.Configurations;

public class ReportConfiguration: IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable("reports")
            .HasOne(x => x.Reporter)
            .WithMany()
            .HasForeignKey("reporter_id");

        builder.ToTable("reports")
            .HasOne(x => x.Offer)
            .WithMany()
            .HasForeignKey("offer_id");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Reason)
            .HasColumnName("reason")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(x => x.Text)
            .HasColumnName("text")
            .HasColumnType("nvarchar(MAX)")
            .IsRequired(false);
    }
}