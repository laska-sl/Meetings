using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Meetings.CQRS.Abstractions.Commands;
using Meetings.Data.Data;
using Meetings.Data.Models;

namespace Meetings.CQRS.Handlers
{
    internal class CreateMeetingHandler : IRequestHandler<CreateMeetingCommand>
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public CreateMeetingHandler(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
        {
            var meeting = this.mapper.Map<Meeting>(request.MeetingForCreationDTO);

            await this.context.Meetings.AddAsync(meeting, cancellationToken);

            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
