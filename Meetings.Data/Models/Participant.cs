using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Meetings.Data.Models
{
    public class Participant
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        public string FullName { get; set; }

        public ICollection<MeetingParticipant> MeetingParticipants { get; set; }
    }
}
