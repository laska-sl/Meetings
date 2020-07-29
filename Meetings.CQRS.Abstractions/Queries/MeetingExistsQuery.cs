using MediatR;

namespace Meetings.CQRS.Abstractions.Queries
{
    public class MeetingExistsQuery : IRequest<bool>
    {
        public MeetingExistsQuery(int meetingId)
        {
            this.MeetingId = meetingId;
        }

        public int MeetingId { get; }
    }
}
