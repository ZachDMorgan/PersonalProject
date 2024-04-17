using Domain.Entities;
using Domain.Enumerations;

namespace Application.Dtos
{

    public class UserDto
    {

        #region Properties

        public Guid UserID { get; set; }

        public ContactDetailsDto ContactDetails { get; set; }

        public string FirstName { get; set; }

        public PractitionerDto? Practitioner { get; set; }

        public UserRole Role { get; set; }

        public string Surname { get; set; }

        public string Title { get; set; }

        public string Username { get; set; }

        #endregion

        #region Methods

        public static implicit operator UserDto(User user) => new()
        {
            ContactDetails = user.ContactDetails,
            FirstName = user.FirstName,
            Practitioner = user.Practitioner == null ? null : user.Practitioner,
            Role = user.Role,
            Surname = user.Surname,
            Title = user.Title,
            UserID = user.ID,
            Username = user.Username
        };

        #endregion

    }

}
