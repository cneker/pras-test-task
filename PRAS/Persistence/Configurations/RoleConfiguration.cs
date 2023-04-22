using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRAS.Models;

namespace PRAS.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            var role = new Role()
            {
                Id = 1,
                Name = "Admin"
            };

            builder.HasData(role);
        }
    }
}
