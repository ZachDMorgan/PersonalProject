using Domain.Entities;

namespace Application.Dtos
{

    public class UnavailabilityDto
    {

        #region Properties

        public Guid UnavailabilityID { get; set; }

        public DateTime EndOn { get; set; }

        public DateTime StartOn { get; set; }

        #endregion

        #region Methods

        public static implicit operator UnavailabilityDto(Unavailability unavailability) => new()
        {
            EndOn = unavailability.EndOn,
            StartOn = unavailability.StartOn,
            UnavailabilityID = unavailability.ID,
        };

        #endregion

    }

}
