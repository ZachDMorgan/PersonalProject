using Domain.Entities;
using Domain.Enumerations;

namespace Application.Dtos
{

    public class UserDto
    {

        #region Properties

        public Guid UserID { get; set; }

        public PractitionerDto? Practitioner { get; set; }

        public PersonDto Person { get; set; }

        public UserRole Role { get; set; }

        public string Username { get; set; }

        #endregion

        #region Methods

        public static implicit operator UserDto(User user) => new()
        {
            Practitioner = user.Practitioner == null ? null : user.Practitioner,
            Person = user.Person,
            Role = user.Role,
            UserID = user.ID,
            Username = user.Username
        };

        #endregion

    }

}
