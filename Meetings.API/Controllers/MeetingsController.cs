using System;
using System.Collections.Generic;
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
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly IMediator mediator;

        public MeetingsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Creates a new meeting")]
        [HttpPost]
        public async Task<IActionResult> CreateMeeting(MeetingForCreationDTO meetingForCreationDTO)
        {
            if (string.IsNullOrEmpty(meetingForCreationDTO.Title) || meetingForCreationDTO.StartTime == DateTime.MinValue)
            {
                return this.BadRequest("Invalid input");
            }

            var command = new CreateMeetingCommand(meetingForCreationDTO);

            await this.mediator.Send(command);

            return this.NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Cancels meeting with the specified Id")]
        [HttpPost]
        [Route("{meetingId}/Cancel")]
        public async Task<IActionResult> CancelMeeting(int meetingId)
        {
            var meetingExistsQuery = new MeetingExistsQuery(meetingId);
            var meetingExists = await this.mediator.Send(meetingExistsQuery);

            if (!meetingExists)
            {
                return this.NotFound("There is no meeting with such Id");
            }

            var cancelMeetingCommand = new CancelMeetingCommand(meetingId);

            await this.mediator.Send(cancelMeetingCommand);

            return this.NoContent();
        }

        [ProducesResponseType(typeof(IEnumerable<MeetingForReturnDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Returns all meetings")]
        [HttpGet]
        public async Task<IActionResult> GetMeetings()
        {
            var query = new GetMeetingsQuery();

            var meetingDTOs = await this.mediator.Send(query);

            return this.Ok(meetingDTOs);
        }
    }
}
