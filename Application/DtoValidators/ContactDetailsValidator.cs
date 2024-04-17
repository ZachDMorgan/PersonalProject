using Application.Dtos;
using Application.Services.Validation;

namespace Application.DtoValidators
{

    public class ContactDetailsValidator : DtoValidator, IDtoValidator<ContactDetailsDto>
    {

        #region Methods

        bool IDtoValidator<ContactDetailsDto>.Validate(ContactDetailsDto contactDetails, out ICollection<ValidationError> errors)
        {
            errors = new List<ValidationError>();
            var _IsValid = true;

            if (contactDetails == null)
            {
                errors.Add(this.PropertyIsEmpty(nameof(contactDetails)));
                return false;
            }

            if (string.IsNullOrWhiteSpace(contactDetails.Email))
            {
                errors.Add(this.PropertyIsRequired(nameof(contactDetails.Email)));
                _IsValid = false;
            }

            if (string.IsNullOrWhiteSpace(contactDetails.Phone) && string.IsNullOrWhiteSpace(contactDetails.Mobile))
            {

                foreach (var _Error in this.PropertyIsRequired(new List<string>() { nameof(contactDetails.Phone), nameof(contactDetails.Mobile) }))
                    errors.Add(_Error);
                _IsValid = false;
            }

            return _IsValid;
        }

        #endregion

    }

}
