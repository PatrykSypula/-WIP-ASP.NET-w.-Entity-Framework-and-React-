using ASPNET_EF.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNET.Migrations
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Dictionaries> Dictionaries { get; set; }
        public DbSet<Words> Words { get; set; }
        public DbSet<SessionStatistics> SessionStatistics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SubscribedDictionary> SubscribedDictionary { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dictionaries>(cs =>
            {
                cs.HasMany(c => c.Words)
                .WithOne(w => w.Dictionary)
                .HasForeignKey(w => w.DictionaryId);

                cs.HasMany(d => d.SessionStatistics)
                .WithOne(s => s.Dictionary)
                .HasForeignKey(s => s.DictionaryId);

                cs.HasMany(d => d.SubscribedDictionaries)
                .WithOne(sc => sc.Dictionary)
                .HasForeignKey(sc => sc.DictionaryId);
            });
            modelBuilder.Entity<User>(us =>
            {
                us.HasMany(u => u.Dictionaries)
                .WithOne(c => c.User)
                .HasForeignKey(d => d.UserId);

                us.HasMany(u => u.SessionStatistics)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);

                us.HasMany(u => u.SubscribedDictionaries)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            });
            //base.OnModelCreating(modelBuilder);

        }

    }
}
