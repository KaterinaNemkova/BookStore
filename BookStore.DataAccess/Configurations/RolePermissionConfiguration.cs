﻿using Permission = BookStore.Core.Enums.Permission;
using Role = BookStore.Core.Enums.Role;
using BookStore.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BookStore.DataAccess.Configurations
{
    public partial class RolePermissionConfiguration:IEntityTypeConfiguration<RolePermissionEntity>
    {
        private readonly AuthorizationOptions _authorizationOptions;

        public RolePermissionConfiguration(AuthorizationOptions authorizationOptions)
        {
            _authorizationOptions = authorizationOptions;
        }

        public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
        {
            builder.HasKey(r => new{r.RoleId,r.PermissionId});
            builder.HasData(ParseRolePermissions());
        }

        private RolePermissionEntity[] ParseRolePermissions()
        {
            return _authorizationOptions.RolePermissions
                .SelectMany(rp => rp.Permissions.Select(p => new RolePermissionEntity
                {
                    RoleId=(int)Enum.Parse<Role>(rp.Role),
                    PermissionId=(int)Enum.Parse<Permission>(p)

                }))
                .ToArray();


        }


    }
}
