using System;

namespace Meetings.Data.Abstractions.DTOs
{
    public class MeetingForCreationDTO
    {
        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
