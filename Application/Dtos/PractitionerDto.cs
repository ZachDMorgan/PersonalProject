using Domain.Entities;

namespace Application.Dtos
{
    //TODO: make a dto proper
    public class PractitionerDto
    {

        #region Properties

        public Guid PractitionerID { get; set; }

        public ICollection<AvailabilityDto> Availabilities { get; set; } = new HashSet<AvailabilityDto>();

        public ContactDetailsDto ContactDetails { get; set; }

        public PersonDto Person { get; set; }

        public ProfessionDto Profession { get; set; }

        public ICollection<ServiceDto> Services { get; set; } = new HashSet<ServiceDto>();

        public ICollection<UnavailabilityDto> Unavailabilities { get; set; } = new HashSet<UnavailabilityDto>();

        #endregion

        #region Methods

        public static implicit operator PractitionerDto(Practitioner practitioner) => new()
        {
            Availabilities = practitioner.Availabilities.Select<Availability, AvailabilityDto>(a => a).ToHashSet(),
            PractitionerID = practitioner.ID,
            Person = practitioner.Person,
            Profession = practitioner.Profession,
            Services = practitioner.Services.Select<PractitionerService, ServiceDto>(s => s.Service).ToHashSet(),
            Unavailabilities = practitioner.Unavailabilities.Select<Unavailability, UnavailabilityDto>(u => u).ToHashSet(),
        };

        #endregion

    }

}
