using Application.Dtos;
using Application.Services.Validation;

namespace Application.DtoValidators
{

    public class UpdateContactDetailsValidator : DtoValidator, IDtoValidator<UpdateContactDetailsDto>
    {

        #region Methods

        bool IDtoValidator<UpdateContactDetailsDto>.Validate(UpdateContactDetailsDto contactDetails, out ICollection<ValidationError> errors)
        {
            errors = new List<ValidationError>();
            var _IsValid = true;

            if (contactDetails == null)
                return true;

            if (contactDetails.Email.HasBeenSet && string.IsNullOrWhiteSpace(contactDetails.Email.Value))
            {
                errors.Add(this.PropertyIsRequired(nameof(contactDetails.Email)));
                _IsValid = false;
            }

            //Bad cos you can't unset any phone fields. Business rule is only one is required. Fix later.
            if (contactDetails.Phone.HasBeenSet && string.IsNullOrWhiteSpace(contactDetails.Phone.Value))
            {
                errors.Add(this.PropertyIsEmpty(nameof(contactDetails.Phone)));
                _IsValid = false;
            }

            if (contactDetails.Mobile.HasBeenSet && string.IsNullOrWhiteSpace(contactDetails.Mobile.Value))
            {
                errors.Add(this.PropertyIsEmpty(nameof(contactDetails.Mobile)));
                _IsValid = false;
            }

            return _IsValid;
        }

        #endregion

    }

}
