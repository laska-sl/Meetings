using System.Threading.Tasks;

using MediatR;

using Meetings.API.Controllers;
using Meetings.CQRS.Abstractions.Queries;
using Meetings.Data.Abstractions.DTOs;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace Meetings.Tests
{
    public class MeetingsControllerTests
    {
        [Fact]
        public async Task CreateMeeting_EmptyString_ReturnsBadRequest()
        {
            var mockMediator = new Mock<IMediator>();

            var controller = new MeetingsController(mockMediator.Object);

            var meetingForCreationDTO = new MeetingForCreationDTO
            {
                Title = string.Empty
            };

            var result = await controller.CreateMeeting(meetingForCreationDTO);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
