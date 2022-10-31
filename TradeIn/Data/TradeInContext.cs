using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TradeIn.Models;

namespace TradeIn.Data
{
    public class TradeInContext : IdentityDbContext<IdentityUser>
    {
        public TradeInContext()
        {
        }

        //public TradeInContext(DbContextOptions<TradeInContext> options)
        //    : base(options)
        //{
        //}

        public DbSet<TradeIn.Models.Brand> Brands { get; set; } = default!;
        public DbSet<TradeIn.Models.Model> Models { get; set; } = default!;
        public DbSet<TradeIn.Models.Generation> Generations { get; set; } = default!;
        public DbSet<TradeIn.Models.Condition> Conditions { get; set; } = default!;
        public DbSet<Valuation> Valuations { get; set; } = default!;
        public DbSet<Contact> Contacts { get; set; } = default!;
        public DbSet<TradeIn.Models.UserInfo> UserInfos { get; set; } = default!;
        public DbSet<TradeIn.Models.EstimateRef> EstimateRefs { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>()
                .HasMany<Model>(b => b.Models)
                .WithOne(m => m.Brand);

            modelBuilder.Entity<Model>()
                .HasMany<Generation>(m => m.Generations)
                .WithOne(g => g.Model);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configurationRoot = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

                optionsBuilder.UseSqlServer(configurationRoot.GetConnectionString("TradeInContext") ?? throw new InvalidOperationException("Connection string 'TradeInContext' not found."));
            }
        }
    }
}
