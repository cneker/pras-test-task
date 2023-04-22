using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRAS.Models;

namespace PRAS.Persistence.Configurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.HasIndex(n => n.PublicationDate)
                .IsDescending();
            builder.Property(n => n.PublicationDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
