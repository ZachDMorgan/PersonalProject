using Domain.Entities;

namespace Application.Dtos
{

    public class AvailabilityDto
    {

        #region Properties

        public Guid AvailabilityID { get; set; }

        public DayOfWeek Day { get; set; }

        public TimeOnly EndTime { get; set; }

        public TimeOnly StartTime { get; set; }

        #endregion

        #region Methods

        public static implicit operator AvailabilityDto(Availability availability) => new()
        {
            AvailabilityID = availability.ID,
            Day = availability.Day,
            EndTime = availability.EndTime,
            StartTime = availability.StartTime,
        };

        #endregion

    }

}
