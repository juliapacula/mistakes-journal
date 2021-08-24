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
                    .IsRequired(false);
            });

            modelBuilder.Entity<Label>(label =>
            {
                label.HasKey(l => l.Id);
                label.Property(l => l.Name).IsRequired();
                label.Property(l => l.Color).IsRequired();
            });

            modelBuilder.Entity<MistakeLabel>(mistakeLabel =>
            {
                mistakeLabel.HasKey(ml => new { ml.MistakeId, ml.LabelId });
                mistakeLabel.HasOne(ml => ml.Mistake)
                    .WithMany(m => m.MistakeLabels)
                    .HasForeignKey(ml => ml.MistakeId)
                    .OnDelete(DeleteBehavior.Cascade);
                mistakeLabel.HasOne(ml => ml.Label)
                    .WithMany(m => m.MistakeLabels)
                    .HasForeignKey(ml => ml.LabelId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Repetition>(r =>
            {
                r.HasKey(t => t.Id);
                r.Property(t => t.DateTime).IsRequired();
                r.HasOne(t => t.Mistake)
                    .WithMany(m => m.Repetitions)
                    .HasForeignKey(t => t.MistakeId)
                    .IsRequired();
            });
        }
    }
}