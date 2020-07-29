using MediatR;

namespace Meetings.CQRS.Abstractions.Commands
{
    public class CancelMeetingCommand : IRequest
    {
        public CancelMeetingCommand(int meetingId)
        {
            this.MeetingId = meetingId;
        }

        public int MeetingId { get; }
    }
}
