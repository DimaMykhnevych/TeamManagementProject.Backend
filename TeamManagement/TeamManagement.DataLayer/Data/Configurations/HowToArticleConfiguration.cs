using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.DataLayer.Data.Configurations
{
    public class HowToArticleConfiguration : IEntityTypeConfiguration<HowToArticle>
    {
        public void Configure(EntityTypeBuilder<HowToArticle> builder)
        {
            builder.HasKey(article => article.Id);

            builder.Property(article => article.Name).IsRequired().HasMaxLength(60);
            builder.Property(article => article.Problem).IsRequired();
            builder.Property(article => article.Solution).IsRequired();
            builder.Property(article => article.DateOfPublishing).IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasIndex(article => article.Name);

            builder.HasOne(article => article.Publisher)
                .WithMany(user => user.HowToArticles)
                .HasForeignKey(article => article.PublisherId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
