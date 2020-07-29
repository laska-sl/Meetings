using System.Threading.Tasks;

using MediatR;

using Meetings.CQRS.Abstractions.Commands;
using Meetings.CQRS.Abstractions.Queries;
using Meetings.Data.Abstractions.DTOs;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace Meetings.API.Controllers
{
    [Route("api/Meetings/{meetingId}/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ParticipantsController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Adds a participant to the specified meeting")]
        [HttpPost]
        public async Task<IActionResult> AddParticipants(ParticipantForCreationDTO participant, int meetingId)
        {
            var meetingExistsQuery = new MeetingExistsQuery(meetingId);
            var meetingExists = await this.mediator.Send(meetingExistsQuery);

            if (meetingExists)
            {
                return this.NotFound("There is no meeting with such Id");
            }

            var meetingCanceledQuery = new MeetingCanceledQuery(meetingId);
            var meetingCanceled = await this.mediator.Send(meetingCanceledQuery);

            if (meetingCanceled)
            {
                return this.BadRequest("Can't add participant to canceled meeting");
            }

            var addParticipantsCommand = new AddParticipantsCommand(meetingId, participant);

            await this.mediator.Send(addParticipantsCommand);

            return this.NoContent();
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Removes a participant from the specified meeting")]
        [HttpPost]
        [Route("{participantId}/Remove")]
        public async Task<IActionResult> RemoveParticipant(int meetingId, int participantId)
        {
            var meetingExistsQuery = new MeetingExistsQuery(meetingId);
            var meetingExists = await this.mediator.Send(meetingExistsQuery);

            if (meetingExists)
            {
                return this.NotFound("There is no meeting with such Id");
            }

            var meetingCanceledQuery = new MeetingCanceledQuery(meetingId);
            var meetingCanceled = await this.mediator.Send(meetingCanceledQuery);

            if (meetingCanceled)
            {
                return this.BadRequest("Can't remove participant from canceled meeting");
            }

            var removeParticipantCommand = new RemoveParticipantCommand(meetingId, participantId);

            await this.mediator.Send(removeParticipantCommand);

            return this.NoContent();
        }
    }
}
