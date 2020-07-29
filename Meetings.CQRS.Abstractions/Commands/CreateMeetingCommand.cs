using MediatR;

using Meetings.Data.Abstractions.DTOs;

namespace Meetings.CQRS.Abstractions.Commands
{
    public class CreateMeetingCommand : IRequest
    {
        public CreateMeetingCommand(MeetingForCreationDTO meetingForCreationDTO)
        {
            this.MeetingForCreationDTO = meetingForCreationDTO;
        }

        public MeetingForCreationDTO MeetingForCreationDTO { get; }
    }
}
