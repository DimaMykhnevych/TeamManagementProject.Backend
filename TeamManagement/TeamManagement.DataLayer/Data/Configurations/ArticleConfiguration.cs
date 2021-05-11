using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.DataLayer.Data.Configurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasOne(article => article.Publisher)
                   .WithMany(user => user.Articles).OnDelete(DeleteBehavior.SetNull);
            builder.Property(article => article.Name).IsRequired();
            builder.Property(article => article.Content).IsRequired();
            builder.Property(article => article.Status).IsRequired();
            builder.Property(article => article.DateOfPublishing).IsRequired();
        }
    }
}
