using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Meetings.CQRS.Abstractions.Queries;
using Meetings.Data.Abstractions.DTOs;
using Meetings.Data.Data;

using Microsoft.EntityFrameworkCore;

namespace Meetings.CQRS.Handlers
{
    public class GetMeetingsHandler : IRequestHandler<GetMeetingsQuery, IEnumerable<MeetingForReturnDTO>>
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public GetMeetingsHandler(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<MeetingForReturnDTO>> Handle(GetMeetingsQuery request, CancellationToken cancellationToken)
        {
            var meetings = await this.context.Meetings
                .Include(m => m.MeetingParticipants)
                .ThenInclude(mp => mp.Participant)
                .ToListAsync(cancellationToken);

            var meetingsForReturn = this.mapper.Map<IEnumerable<MeetingForReturnDTO>>(meetings);

            return meetingsForReturn;
        }
    }
}
