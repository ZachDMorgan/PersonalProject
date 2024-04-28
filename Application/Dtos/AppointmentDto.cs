using Domain.Entities;
using Domain.Enumerations;

namespace Application.Dtos
{

    public class AppointmentDto
    {

        #region Properties

        public Guid AppointmentID { get; set; }

        public int? DurationOverride { get; set; }

        public PracticeDto Practice { get; set; }

        public PractitionerDto Practitioner { get; set; }

        public ServiceDto Service { get; set; }

        public DateTime StartOn { get; set; }

        public AppointmentState State { get; set; }

        public UserDto User { get; set; }

        #endregion

        #region Methods

        public static implicit operator AppointmentDto(Appointment appointment) => new()
        {
            AppointmentID = appointment.ID,
            DurationOverride = appointment.DurationOverride,
            Practice = appointment.Practice,
            Practitioner = appointment.Practitioner,
            Service = appointment.Service,
            StartOn = appointment.StartOn,
            State = appointment.State,
            User = appointment.User
        };

        #endregion

    }

}
