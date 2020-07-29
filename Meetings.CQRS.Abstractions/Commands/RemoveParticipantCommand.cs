using MediatR;

namespace Meetings.CQRS.Abstractions.Commands
{
    public class RemoveParticipantCommand : IRequest
    {
        public RemoveParticipantCommand(int meetingId, int participantId)
        {
            this.MeetingId = meetingId;
            this.ParticipantId = participantId;
        }

        public int MeetingId { get; }

        public int ParticipantId { get; }
    }
}
