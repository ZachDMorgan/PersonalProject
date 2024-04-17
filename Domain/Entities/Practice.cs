namespace Domain.Entities
{

    public class Practice
    {

        #region Properties

        public Guid ID { get; set; }

        public string Address { get; set; }

        public HashSet<Appointment> Appointments { get; set; } = new HashSet<Appointment>();

        public HashSet<Availability> Availabilities { get; set; } = new HashSet<Availability>();

        public ContactDetails ContactDetails { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public string Name { get; set; }

        public HashSet<Unavailability> Unavailabilities { get; set; } = new HashSet<Unavailability>();

        #endregion

    }

}
