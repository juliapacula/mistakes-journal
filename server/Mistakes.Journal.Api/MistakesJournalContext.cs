using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mistakes.Journal.Api.Logic.Identity.Models;
using Mistakes.Journal.Api.Logic.Identity.Services;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api
{
    public class MistakesJournalContext : IdentityDbContext<MistakesJournalUser, IdentityRole<Guid>, Guid>
    {
        private readonly IUserProvider _userProvider;

        public MistakesJournalContext(DbContextOptions<MistakesJournalContext> options, IUserProvider userProvider)
            : base(options)
        {
            _userProvider = userProvider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            buildIdentity(modelBuilder);

            modelBuilder.Entity<Mistake>(mistake =>
            {
                mistake.HasKey(m => m.Id);
                mistake.Property(m => m.Name).IsRequired();
                mistake.Property(m => m.Goal);
                mistake.Property(m => m.Priority).HasConversion<string>();
                mistake.Property(m => m.CreatedAt).IsRequired();
                mistake.Property(m => m.IsSolved).HasDefaultValue(false);
                mistake.OwnsOne(m => m.AdditonalQuestions, 
                    m =>
                    {
                        m.Property(a => a.Consequences);
                        m.Property(a => a.WhatCanIDoBetter);
                        m.Property(a => a.WhatDidILearn);
                        m.Property(a => a.CanIFixIt);
                        m.Property(a => a.OnlyResponsible);
                    });
                mistake.HasOne(m => m.User)
                    .WithMany(u => u.Mistakes)
                    .HasForeignKey(m => m.UserId)
                    .IsRequired();
                mistake.HasQueryFilter(m => m.UserId == _userProvider.GetId());
            });

            modelBuilder.Entity<Tip>(tip =>
            {
                tip.HasKey(t => t.Id);
                tip.Property(t => t.Content).IsRequired();
                tip.HasOne(t => t.Mistake)
                    .WithMany(m => m.Tips)
                    .HasForeignKey(t => t.MistakeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired(false);

                tip.HasQueryFilter(t => t.Mistake.UserId == _userProvider.GetId());
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
                r.Property(t => t.OccuredAt).IsRequired();
                r.HasOne(t => t.Mistake)
                    .WithMany(m => m.Repetitions)
                    .HasForeignKey(t => t.MistakeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
        }

        private void buildIdentity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MistakesJournalUser>(user =>
            {
                user.ToTable("user");
                user.Property(u => u.FirstName).IsRequired();
                user.Property(u => u.LastName).IsRequired();
            });

            modelBuilder.Entity<IdentityRole<Guid>>().ToTable("role");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("role_claim");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("user_role");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claim");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("user_token");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("user_login");
        }
    }
}