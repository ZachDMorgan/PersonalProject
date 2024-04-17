using Domain.Entities;

namespace Application.Dtos
{

    public class ContactDetailsDto
    {

        #region Properties

        public string Email { get; set; }

        public string Fax { get; set; }

        public string Mobile { get; set; }

        public string Phone { get; set; }

        #endregion

        #region Methods

        public static implicit operator ContactDetailsDto(ContactDetails contactDetails) => new()
        {
            Email = contactDetails.Email,
            Fax = contactDetails.Fax,
            Mobile = contactDetails.Mobile,
            Phone = contactDetails.Phone
        };

        public static explicit operator ContactDetails(ContactDetailsDto contactDetails) => new()
        {
            Email = contactDetails.Email,
            Fax = contactDetails.Fax,
            Mobile = contactDetails.Mobile,
            Phone = contactDetails.Phone
        };

        #endregion

    }

}
