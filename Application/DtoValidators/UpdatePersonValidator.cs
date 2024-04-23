using Application.Dtos;
using Application.Services.Validation;

namespace Application.DtoValidators
{

    public class UpdatePersonValidator : DtoValidator, IDtoValidator<UpdatePersonDto>
    {
        #region Fields

        private readonly IDtoValidator<UpdateContactDetailsDto> _contactDetailsValidator;

        #endregion

        #region Constructors

        public UpdatePersonValidator(UpdateContactDetailsValidator contactDetailsValidator)
            => this._contactDetailsValidator = contactDetailsValidator;

        #endregion

        #region Methods

        bool IDtoValidator<UpdatePersonDto>.Validate(UpdatePersonDto person, out ICollection<ValidationError> errors)
        {
            errors = new List<ValidationError>();

            if (person == null)
                return true;

            var _IsValid = this._contactDetailsValidator.Validate(person.ContactDetails, out errors);
            if (person.FirstName.HasBeenSet && string.IsNullOrWhiteSpace(person.FirstName.Value))
            {
                errors.Add(this.PropertyIsRequired(nameof(person.FirstName)));
                _IsValid = false;
            }
            if (person.Surname.HasBeenSet && string.IsNullOrWhiteSpace(person.Surname.Value))
            {
                errors.Add(this.PropertyIsRequired(nameof(person.Surname)));
                _IsValid = false;
            }

            return _IsValid;
        }

        #endregion

    }

}
