using System;

using Meetings.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace Meetings.Data.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Meeting> Meetings { get; set; }

        public DbSet<Participant> Participants { get; set; }

        public DbSet<MeetingParticipant> MeetingParticipants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeetingParticipant>()
                .HasKey(mp => new { mp.MeetingId, mp.ParticipantId });

            modelBuilder.Entity<MeetingParticipant>()
                .HasOne(mp => mp.Meeting)
                .WithMany(m => m.MeetingParticipants)
                .HasForeignKey(mp => mp.MeetingId);

            modelBuilder.Entity<MeetingParticipant>()
                .HasOne(mp => mp.Participant)
                .WithMany(p => p.MeetingParticipants)
                .HasForeignKey(mp => mp.ParticipantId);
        }
    }
}
