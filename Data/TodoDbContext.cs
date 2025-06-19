using Microsoft.EntityFrameworkCore;
using TodoServices.Models;

namespace TodoServices.Data
{
    public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ChecklistModel> Checklists { get; set; }
        public DbSet<ItemModel> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>(entity => {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => u.Username).IsUnique();
            });

            modelBuilder.Entity<ChecklistModel>()
                .HasOne(c => c.User)
                .WithMany(u => u.Checklists)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ItemModel>()
                .HasOne(i => i.Checklist)
                .WithMany(c => c.Items)
                .HasForeignKey(i => i.ChecklistId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
