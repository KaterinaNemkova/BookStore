using BookStore.Core.Enums;
using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Configurations
{
    public class RoleConfiguration:IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.HasKey(x=>x.Id);

            builder.HasMany(r => r.Permissions).WithMany(l => l.Roles).UsingEntity<RolePermissionEntity>(
                r => r.HasOne<PermissionEntity>().WithMany().HasForeignKey(r => r.PermissionId),
                l => l.HasOne<RoleEntity>().WithMany().HasForeignKey(l => l.RoleId)
                );

            var roles = Enum.GetValues<Role>()
                .Select(r => new RoleEntity
                {
                    Id = (int)r,
                    Name = r.ToString()
                });

            builder.HasData(roles);
        }
    }
}
