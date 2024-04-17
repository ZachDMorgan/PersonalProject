namespace Domain.Entities
{

    public class Practitioner
    {

        #region Properties

        public Guid ID { get; set; }

        public HashSet<Appointment> Appointments { get; set; } = new HashSet<Appointment>();

        public HashSet<Availability> Availabilities { get; set; } = new HashSet<Availability>();

        public ContactDetails ContactDetails { get; set; }

        public string FirstName { get; set; }

        public Profession Profession { get; set; }

        public HashSet<PractitionerService> Services { get; set; }

        public string Surname { get; set; }

        public string Title { get; set; }

        public HashSet<Unavailability> Unavailabilities { get; set; } = new HashSet<Unavailability>();

        #endregion

    }

}
