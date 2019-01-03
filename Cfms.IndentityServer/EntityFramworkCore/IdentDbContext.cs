﻿using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cfms.IndentityServer.EntityFramworkCore
{
    public class IdentDbContext : IdentityDbContext<UserInfo, RoleInfo, Guid>, 
        IConfigurationDbContext, IPersistedGrantDbContext
    {
        public IdentDbContext(DbContextOptions options) :base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<DeviceFlowCodes>().HasBaseType<DeviceFlow>();
        }

        public Task<int> SaveChangesAsync()
        {
            //throw new NotImplementedException();
            var count = this.SaveChanges();
            return Task.FromResult(count);
        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<IdentityResource> IdentityResources { get; set; }
        public DbSet<ApiResource> ApiResources { get; set; }
        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }
    }
}
