using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Meetings.CQRS.Abstractions.Queries;
using Meetings.Data.Data;

namespace Meetings.CQRS.Handlers
{
    public class MeetingCanceledHandler : IRequestHandler<MeetingCanceledQuery, bool>
    {
        private readonly DataContext context;

        public MeetingCanceledHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(MeetingCanceledQuery request, CancellationToken cancellationToken)
        {
            var meetingCanceled = await this.context.Meetings.FindAsync(new object[] {request.MeetingId}, cancellationToken);

            return meetingCanceled.IsCanceled;
        }
    }
}
