using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.DataLayer.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(user => user.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(user => user.LastName).IsRequired().HasMaxLength(100);

            builder.HasMany(user => user.HowToArticles)
               .WithOne(article => article.Publisher)
               .HasForeignKey(article => article.PublisherId)
               .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
