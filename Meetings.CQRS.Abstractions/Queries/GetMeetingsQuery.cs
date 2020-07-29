using System.Collections.Generic;

using MediatR;

using Meetings.Data.Abstractions.DTOs;

namespace Meetings.CQRS.Abstractions.Queries
{
    public class GetMeetingsQuery : IRequest<IEnumerable<MeetingForReturnDTO>>
    {
    }
}
