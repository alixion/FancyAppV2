﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FancyAppV2.STS.Models;

namespace FancyAppV2.STS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FidoStoredCredential> FidoStoredCredential { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<FidoStoredCredential>().HasKey(m => m.Id);

            base.OnModelCreating(builder);
        }
    }
}