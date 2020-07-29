using MediatR;

using Meetings.Data.Abstractions.DTOs;

namespace Meetings.CQRS.Abstractions.Commands
{
    public class AddParticipantsCommand : IRequest
    {
        public AddParticipantsCommand(int meetingId, ParticipantForCreationDTO participantDTO)
        {
            this.MeetingId = meetingId;
            this.ParticipantDTO = participantDTO;
        }

        public int MeetingId { get; }

        public ParticipantForCreationDTO ParticipantDTO { get; }
    }
}
