using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Dal
{
    public sealed class EntityDbContext : IdentityDbContext<User>
    {
        private readonly Action<DbContextOptionsBuilder> _dbContextOptionsBuilderAction;

        /// <summary>
        /// For unit testing
        /// </summary>
        public EntityDbContext()
        {
            
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Constructor that will be called by startup.cs
        /// </summary>
        /// <param name="dbContextOptionsBuilderAction"></param>
        // ReSharper disable once SuggestBaseTypeForParameter
        public EntityDbContext(Action<DbContextOptionsBuilder> dbContextOptionsBuilderAction)
        {
            _dbContextOptionsBuilderAction = dbContextOptionsBuilderAction;

            Database.EnsureCreated();
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _dbContextOptionsBuilderAction(optionsBuilder);
        }
        
        protected override void  OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(x => x.Answers)
                .WithOne(x => x.UserRef)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<User>()
                .HasMany(x => x.Questions)
                .WithOne(x => x.UserRef)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<Question>()
                .HasMany(x => x.Tags)
                .WithOne(x => x.QuestionRef)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Question>()
                .HasMany(x => x.Answers)
                .WithOne(x => x.QuestionRef)
                .OnDelete(DeleteBehavior.Cascade);
            
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Question> Questions { get; set; }
    }
}