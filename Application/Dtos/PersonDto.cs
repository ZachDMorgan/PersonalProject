using Domain.Entities;

namespace Application.Dtos
{

    public class PersonDto
    {

        #region Properties

        public ContactDetailsDto ContactDetails { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Title { get; set; }

        #endregion

        #region Methods

        public static implicit operator PersonDto(Person person) => new()
        {
            ContactDetails = person.ContactDetails,
            FirstName = person.FirstName,
            Surname = person.Surname,
            Title = person.Title,
        };

        public static implicit operator Person(PersonDto person) => new()
        {
            ContactDetails = person.ContactDetails,
            FirstName = person.FirstName,
            Surname = person.Surname,
            Title = person.Title,
        };

        #endregion

    }

}
