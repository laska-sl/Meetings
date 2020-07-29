using System.Linq;

using AutoMapper;

using Meetings.Data.Abstractions.DTOs;
using Meetings.Data.Models;

namespace Meetings.Data.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // TODO Fix many-to-many mapping
            this.CreateMap<MeetingForCreationDTO, Meeting>();

            // TODO Fix many-to-many mapping
            this.CreateMap<Meeting, MeetingForReturnDTO>()
                .ForMember(meetingForReturnDTO => meetingForReturnDTO.Participants, options =>
                {
                    options.MapFrom(meeting => meeting.MeetingParticipants.Select(mp => mp.Participant).ToList());
                });

            this.CreateMap<ParticipantForCreationDTO, Participant>();

            this.CreateMap<Participant, ParticipantForReturnDTO>();
        }
    }
}
