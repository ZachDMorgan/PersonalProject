using Domain.Enumerations;

namespace Domain.Entities
{

    public class Appointment
    {

        #region Properties

        public Guid ID { get; set; }

        public int? DurationOverride { get; set; }

        public Practice Practice { get; set; }

        public Practitioner Practitioner { get; set; }

        public Service Service { get; set; }

        public DateTime StartOn { get; set; }

        public AppointmentState State { get; set; }

        public User User { get; set; }

        #endregion

    }

}
