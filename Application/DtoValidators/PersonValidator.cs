using Application.Dtos;
using Application.Services.Validation;

namespace Application.DtoValidators
{

    public class PersonValidator : DtoValidator, IDtoValidator<PersonDto>
    {
        #region Fields

        private readonly IDtoValidator<ContactDetailsDto> _contactDetailsValidator;

        #endregion

        #region Constructors

        public PersonValidator(ContactDetailsValidator contactDetailsValidator)
            => this._contactDetailsValidator = contactDetailsValidator;

        #endregion

        #region Methods

        bool IDtoValidator<PersonDto>.Validate(PersonDto person, out ICollection<ValidationError> errors)
        {
            errors = new List<ValidationError>();

            if (person == null)
            {
                errors.Add(this.PropertyIsEmpty(nameof(person)));
                return false;
            }

            var _IsValid = this._contactDetailsValidator.Validate(person.ContactDetails, out errors);
            if (string.IsNullOrWhiteSpace(person.FirstName))
            {
                errors.Add(this.PropertyIsRequired(nameof(person.FirstName)));
                _IsValid = false;
            }
            if (string.IsNullOrWhiteSpace(person.Surname))
            {
                errors.Add(this.PropertyIsRequired(nameof(person.Surname)));
                _IsValid = false;
            }

            return _IsValid;
        }

        #endregion

    }

}
