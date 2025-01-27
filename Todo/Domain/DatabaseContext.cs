using Microsoft.EntityFrameworkCore;

namespace Todo.Domain
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);

        }
    }
}
