using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Meetings.CQRS.Abstractions.Commands;
using Meetings.Data.Data;

namespace Meetings.CQRS.Handlers
{
    internal class RemoveParticipantHandler : IRequestHandler<RemoveParticipantCommand>
    {
        private readonly DataContext context;

        public RemoveParticipantHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(RemoveParticipantCommand request, CancellationToken cancellationToken)
        {
            var compositeKey = new object[] {request.MeetingId, request.ParticipantId};

            var meetingParticipant = await this.context.MeetingParticipants.FindAsync(compositeKey, cancellationToken);

            this.context.MeetingParticipants.Remove(meetingParticipant);

            return Unit.Value;
        }
    }
}
