using System;
using System.Collections.Generic;

namespace Meetings.Data.Abstractions.DTOs
{
    public class MeetingForReturnDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public bool IsCanceled { get; set; }

        public IEnumerable<ParticipantForReturnDTO> Participants { get; set; }
    }
}
