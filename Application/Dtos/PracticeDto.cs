using Domain.Entities;

namespace Application.Dtos
{

    public class PracticeDto
    {

        #region Properties

        public Guid PracticeID { get; set; }

        public string Address { get; set; }

        public ICollection<AvailabilityDto> Availabilities { get; set; } = new HashSet<AvailabilityDto>();

        public ContactDetailsDto ContactDetails { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public ICollection<UnavailabilityDto> Unavailabilities { get; set; } = new HashSet<UnavailabilityDto>();

        #endregion

        #region Methods

        public static implicit operator PracticeDto(Practice practice) => new()
        {
            Address = practice.Address,
            Availabilities = practice.Availabilities.Select<Availability, AvailabilityDto>(a => a).ToHashSet(),
            ContactDetails = practice.ContactDetails,
            Description = practice.Description,
            Name = practice.Name,
            Unavailabilities = practice.Unavailabilities.Select<Unavailability, UnavailabilityDto>(u => u).ToHashSet(),
        };

        #endregion

    }

}
