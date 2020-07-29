using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Meetings.CQRS.Abstractions.Commands;
using Meetings.Data.Data;
using Meetings.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace Meetings.CQRS.Handlers
{
    internal class AddParticipantsHandler : IRequestHandler<AddParticipantsCommand>
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public AddParticipantsHandler(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(AddParticipantsCommand request, CancellationToken cancellationToken)
        {
            var meeting = await this.context.Meetings.FindAsync(new object[] {request.MeetingId}, cancellationToken);

            var participantFromDB = await this.context.Participants.FirstOrDefaultAsync(p => p.Email == request.ParticipantDTO.Email, cancellationToken);

            if (participantFromDB != null)
            {
                var compositeKey = new object[] {request.MeetingId, participantFromDB.Id};

                var mp = await this.context.MeetingParticipants.FindAsync(compositeKey, cancellationToken);

                if (mp == null)
                {
                    meeting.MeetingParticipants.Add(new MeetingParticipant
                    {
                        Participant = participantFromDB
                    });
                }
            }
            else
            {
                meeting.MeetingParticipants.Add(new MeetingParticipant
                {
                    Participant = this.mapper.Map<Participant>(request.ParticipantDTO)
                });
            }

            //meeting.MeetingParticipants.Add(
            //    participantFromDB != null
            //        ? new MeetingParticipant {ParticipantId = participantFromDB.Id, Participant = participantFromDB}
            //        : new MeetingParticipant {Participant = this.mapper.Map<Participant>(request.ParticipantDTO)}
            //);

            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
