using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using musingo_backend.Models;

namespace musingo_backend.Configurations;

public class UserCommentConfiguration : IEntityTypeConfiguration<UserComment>
{
    public void Configure(EntityTypeBuilder<UserComment> builder)
    {
        builder.ToTable("user_comments")
            .HasOne(x => x.Transaction)
            .WithMany()
            .HasForeignKey("transaction_id");

        builder.ToTable("user_comments")
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey("user_id");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(x => x.Rating)
            .HasColumnName("rating")
            .HasColumnType("double precision")
            .IsRequired();

        builder.Property(x => x.CommentText)
            .HasColumnName("comment_text")
            .HasColumnType("nvarchar(MAX)")
            .IsRequired(false);

    }
}