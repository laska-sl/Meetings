using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Meetings.Data.Models
{
    public class Meeting
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public bool IsCanceled { get; set; }

        public ICollection<MeetingParticipant> MeetingParticipants { get; set; } = new List<MeetingParticipant>();
    }
}
