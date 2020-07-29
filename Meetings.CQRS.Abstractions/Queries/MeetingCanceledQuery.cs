using MediatR;

namespace Meetings.CQRS.Abstractions.Queries
{
    public class MeetingCanceledQuery : IRequest<bool>
    {
        public MeetingCanceledQuery(int meetingId)
        {
            this.MeetingId = meetingId;
        }

        public int MeetingId { get; }
    }
}
