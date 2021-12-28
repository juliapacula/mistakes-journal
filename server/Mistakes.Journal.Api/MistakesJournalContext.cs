using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mistakes.Journal.Api.Logic.Identity.Models;
using Mistakes.Journal.Api.Logic.Identity.Services;
using Mistakes.Journal.Api.Logic.Mistakes.Models;
using Mistakes.Journal.Api.Logic.Shared.Models;

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

            BuildIdentity(modelBuilder);

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
                    .OnDelete(DeleteBehavior.Cascade)
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
                tip.HasOne(t => t.User)
                    .WithMany(u => u.Tips)
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
                tip.HasQueryFilter(t => t.UserId == _userProvider.GetId());
            });

            modelBuilder.Entity<Label>(label =>
            {
                label.HasKey(l => l.Id);
                label.Property(l => l.Name).IsRequired();
                label.Property(l => l.Color).IsRequired();
                label.HasOne(l => l.User)
                    .WithMany(u => u.Labels)
                    .HasForeignKey(l => l.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
                label.HasQueryFilter(l => l.UserId == _userProvider.GetId());
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
                mistakeLabel.HasQueryFilter(ml => ml.Mistake.UserId == _userProvider.GetId());
            });

            modelBuilder.Entity<Repetition>(r =>
            {
                r.HasKey(t => t.Id);
                r.Property(t => t.OccurredAt).IsRequired();
                r.HasOne(t => t.Mistake)
                    .WithMany(m => m.Repetitions)
                    .HasForeignKey(t => t.MistakeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
                r.HasQueryFilter(rep => rep.Mistake.UserId == _userProvider.GetId());
            });
        }

        private void BuildIdentity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MistakesJournalUser>(user =>
            {
                user.ToTable("user");
                user.Property(u => u.Country).IsRequired();
                user.Property(u => u.Age).IsRequired();
                user.Property(u => u.Group)
                    .IsRequired()
                    .HasDefaultValue(ResearchGroup.Default)
                    .HasConversion<string>();
                user.Property(u => u.Language)
                    .IsRequired()
                    .HasDefaultValue(ApplicationLanguage.EN)
                    .HasConversion<string>();
                user.Property(u => u.WatchedTutorial)
                    .IsRequired()
                    .HasDefaultValue(false);
                user.Property(u => u.AgreeToNewsletter)
                    .IsRequired()
                    .HasDefaultValue(false);
                user.Property(u => u.LastLoggingIn);
                user.Property(u => u.LoggedDaysCount)
                    .IsRequired()
                    .HasDefaultValue(0);
                user.Property(u => u.RegisteredAt)
                    .IsRequired();
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