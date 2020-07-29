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
    public class ParticipantsControllerTests
    {
        [Theory]
        [InlineData(-1)]
        public async Task AddParticipant_NegativeMeetingId_ReturnsNotFound(int meetingId)
        {
            var mockMediator = new Mock<IMediator>();

            var controller = new ParticipantsController(mockMediator.Object);

            var participantForCreationDTO = new ParticipantForCreationDTO();

            var result = await controller.AddParticipants(participantForCreationDTO, meetingId);

            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
