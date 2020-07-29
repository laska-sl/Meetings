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
            // Arrange  
            var meeting = new MeetingForReturnDTO
            {
                IsCanceled = true
            };

            var mockMediator = new Mock<IMediator>();

            var query = new GetMeetingQuery(meetingId);

            mockMediator.Setup(m => m.Send(query, default)).ReturnsAsync(meeting);

            var controller = new ParticipantsController(mockMediator.Object);
            var participantForCreationDTO = new ParticipantForCreationDTO();

            // Act
            var result = await controller.AddParticipants(participantForCreationDTO, meetingId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
