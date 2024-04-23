using Domain.Entities;

namespace Application.Dtos
{

    public class UpdateContactDetailsDto
    {

        #region Properties

        public ChangeTracker<string> Email { get; set; }

        public ChangeTracker<string> Fax { get; set; }

        public ChangeTracker<string> Mobile { get; set; }

        public ChangeTracker<string> Phone { get; set; }

        #endregion

        #region Methods

        public ContactDetails ApplyChanges(ContactDetails contactDetails)
        {
            if (Email.HasBeenSet)
                contactDetails.Email = Email.Value;
            if (Fax.HasBeenSet)
                contactDetails.Fax = Fax.Value;
            if (Mobile.HasBeenSet)
                contactDetails.Mobile = Mobile.Value;
            if (Phone.HasBeenSet)
                contactDetails.Phone = Phone.Value;
            return contactDetails;
        }

        #endregion

    }

}
