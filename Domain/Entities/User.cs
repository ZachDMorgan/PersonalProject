using Domain.Enumerations;

namespace Domain.Entities
{

    public class User
    {

        #region Properties

        public Guid ID { get; set; }

        public HashSet<Appointment> Appointments { get; set; } = new HashSet<Appointment>();

        public ContactDetails ContactDetails { get; set; }

        public string FirstName { get; set; }

        public string Password { get; set; }

        public Practitioner? Practitioner { get; set; }

        public UserRole Role { get; set; }

        public string Surname { get; set; }

        public string Title { get; set; }

        public string Username { get; set; }

        #endregion

    }

}
