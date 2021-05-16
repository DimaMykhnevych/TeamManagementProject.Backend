﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.DataLayer.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<HowToArticle> HowToArticles { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamProject> TeamProjects { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Option> Options { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            builder.Entity<AppUser>()
                .HasOne<Company>(u => u.Company)
                .WithOne(c => c.CEO)
                .HasForeignKey<Company>(c => c.CeoId);

            builder.Entity<Subscription>()
                .HasOne<Company>(s => s.Company)
                .WithOne(c => c.Subscription)
                .HasForeignKey<Company>(c => c.SubscriptionId);

            builder.Entity<Transaction>()
                .HasOne<Subscription>(t => t.Subscription)
                .WithOne(c => c.Transaction)
                .HasForeignKey<Subscription>(s => s.TransactionId);

            builder.Entity<Subscription>()
                .HasOne<SubscriptionPlan>(s => s.SubscriptionPlan)
                .WithOne(s => s.Subscription)
                .HasForeignKey<SubscriptionPlan>(sp => sp.SubscriptionId);
        }
    }
}