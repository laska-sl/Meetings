using System;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Meetings.CQRS.Abstractions.Queries;
using Meetings.Data.Abstractions.DTOs;
using Meetings.Data.Data;

namespace Meetings.CQRS.Handlers
{
    public class GetMeetingHandler : IRequestHandler<GetMeetingQuery, MeetingForReturnDTO>
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public GetMeetingHandler(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<MeetingForReturnDTO> Handle(GetMeetingQuery request, CancellationToken cancellationToken)
        {
            var meeting = await this.context.Meetings.FindAsync(new object[]{request.MeetingId}, cancellationToken);

            return this.mapper.Map<MeetingForReturnDTO>(meeting);
        }
    }
}
