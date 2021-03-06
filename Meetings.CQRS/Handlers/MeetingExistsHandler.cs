﻿using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Meetings.CQRS.Abstractions.Queries;
using Meetings.Data.Data;

namespace Meetings.CQRS.Handlers
{
    public class MeetingExistsHandler : IRequestHandler<MeetingExistsQuery, bool>
    {
        private readonly DataContext context;

        public MeetingExistsHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(MeetingExistsQuery request, CancellationToken cancellationToken)
        {
            var meeting = await this.context.Meetings.FindAsync(new object[] {request.MeetingId}, cancellationToken);

            return meeting != null;
        }
    }
}
