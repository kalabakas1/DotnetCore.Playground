using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playground.Domain.Models.HealthChecks;

namespace Playground.Data
{
    public class HealthCheckConfigurationContext : DbContext
    {
        public DbSet<HealthCheckConfiguration> HealthCheckConfigurations { get; set; }
        public DbSet<HealthCheckAbstract> HealthCheckAbstracts { get; set; }
        public DbSet<SubscriptionTypeAbstract> SubscriptionAbstracts { get; set; }    

        public HealthCheckConfigurationContext()
        {
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite("Data Source=C:\\Data\\Projects\\DotnetCore.Playground\\Playground.App\\Database.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new HealthCheckConfigurationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new HealthCheckAbstractEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UrlHealthCheckEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DefaultHealthCheckerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionAbstractEntityTypeConfiguration());
        }
    }

    class HealthCheckConfigurationEntityTypeConfiguration  : IEntityTypeConfiguration<HealthCheckConfiguration>
    {
        public void Configure(EntityTypeBuilder<HealthCheckConfiguration> builder)
        {
            builder.ToTable("HealthCheckConfiguration"); 
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Retries).IsRequired();
            builder.Property(x => x.SleepInMillsBetweenRetry).IsRequired();
            builder.HasMany(x => x.HealthChecks);
            
            builder.Property<int>("_subscriptionTypeId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("SubscriptionTypeId")
                .IsRequired();
            builder.HasOne(x => x.SubscriptionType).WithMany().HasForeignKey("_subscriptionTypeId");
        }
    }

    class SubscriptionAbstractEntityTypeConfiguration : IEntityTypeConfiguration<SubscriptionTypeAbstract>
    {
        public void Configure(EntityTypeBuilder<SubscriptionTypeAbstract> builder)
        {
            builder.ToTable("SubscriptionType");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever().HasDefaultValue(1).IsRequired();
            builder.HasDiscriminator<string>("Alias")
                .HasValue<LimitedHealthCheckSubscriptionType>("LimitedHealthChecks")
                .HasValue<TwoOfEachSubscriptionType>("PreventOldConfiguration");
        }
    }

    class HealthCheckAbstractEntityTypeConfiguration : IEntityTypeConfiguration<HealthCheckAbstract>
    {
        public void Configure(EntityTypeBuilder<HealthCheckAbstract> builder)
        {
            builder.ToTable("HealthCheck");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Alias).IsRequired();
            builder.Property(x => x.Name).IsRequired();
        }
    }

    class UrlHealthCheckEntityTypeConfiguration : IEntityTypeConfiguration<UrlPingHealthCheck>
    {
        public void Configure(EntityTypeBuilder<UrlPingHealthCheck> builder)
        {
            builder.Property(x => x.Path);
            builder.Property(x => x.ValidResponses)
                .HasConversion(x => JsonSerializer.Serialize(x, null), x => JsonSerializer.Deserialize<int[]>(x, null));
            builder.Property(x => x.Headers)
                .HasConversion(x => JsonSerializer.Serialize(x, null), x => JsonSerializer.Deserialize<Dictionary<string,string>>(x, null));
        }
    }
    
    class DefaultHealthCheckerEntityTypeConfiguration : IEntityTypeConfiguration<DefaultHealthCheck>
    {
        public void Configure(EntityTypeBuilder<DefaultHealthCheck> builder)
        {
        }
    }
}