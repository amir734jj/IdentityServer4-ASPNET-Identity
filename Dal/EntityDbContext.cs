using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Dal
{
    public sealed class EntityDbContext : IdentityDbContext<User>
    {
        // ReSharper disable once SuggestBaseTypeForParameter
        public EntityDbContext(DbContextOptions<EntityDbContext> optionsBuilderOptions) : base(optionsBuilderOptions) { }

        protected override void  OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasMany(x => x.Tags)
                .WithOne(x => x.QuestionRef)
                .OnDelete(DeleteBehavior.Cascade);
            
            // TODO: setup the same relationship (one-to-many) for Question.Answers
            
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Question> Questions { get; set; }
    }
}