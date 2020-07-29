using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Meetings.CQRS.Abstractions.Commands;
using Meetings.Data.Data;

namespace Meetings.CQRS.Handlers
{
    internal class CancelMeetingHandler : IRequestHandler<CancelMeetingCommand>
    {
        private readonly DataContext context;

        public CancelMeetingHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(CancelMeetingCommand request, CancellationToken cancellationToken)
        {
            var meeting = await this.context.Meetings.FindAsync(new object[] {request.MeetingId}, cancellationToken);

            meeting.IsCanceled = true;

            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
