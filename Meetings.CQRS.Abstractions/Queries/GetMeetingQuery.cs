using MediatR;

using Meetings.Data.Abstractions.DTOs;

namespace Meetings.CQRS.Abstractions.Queries
{
    public class GetMeetingQuery : IRequest<MeetingForReturnDTO>
    {
        public GetMeetingQuery(int meetingId)
        {
            this.MeetingId = meetingId;
        }

        public int MeetingId { get; }
    }
}
