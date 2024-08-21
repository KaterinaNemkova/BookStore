using BookStore.Core.Models;
using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataAccess.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(l => l.Users)
                           .WithMany(r => r.Books)
                           .UsingEntity<PortfolioEntity>(
                           l => l.HasOne<UserEntity>().WithMany().HasForeignKey(l => l.UserId),
                           r => r.HasOne<BookEntity>().WithMany().HasForeignKey(r => r.BookId)
                           );
        }
    }
}
