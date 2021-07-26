using Microsoft.EntityFrameworkCore;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api
{
    public class MistakesJournalContext : DbContext
    {
        public MistakesJournalContext(DbContextOptions<MistakesJournalContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mistake>(mistake =>
            {
                mistake.HasKey(m => m.Id);
                mistake.Property(m => m.Name).IsRequired();
                mistake.Property(m => m.Goal);
                mistake.Property(m => m.Priority).HasConversion<string>();
            });

            modelBuilder.Entity<Tip>(tip =>
            {
                tip.HasKey(t => t.Id);
                tip.Property(t => t.Content).IsRequired();
                tip.HasOne(t => t.Mistake)
                    .WithMany(m => m.Tips)
                    .HasForeignKey(t => t.MistakeId)
                    .IsRequired();
            });
        }
    }
}