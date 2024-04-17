using Application.Dtos;
using Application.DtoValidators;
using Application.Services.Persistence;
using Application.Services.Validation;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Users.CreateUser
{

    public class CreateUserValidator : DtoValidator, IValidator<CreateUserInputPort, ICreateUserOutputPort>
    {

        #region Fields

        private readonly IDtoValidator<PersonDto> _personValidator;
        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Constructors

        public CreateUserValidator(IPersistenceContext persistenceContext)
        {
            this._personValidator = new PersonValidator(new ContactDetailsValidator());
            this._persistenceContext = persistenceContext;
        }

        #endregion

        #region Methods

        async Task<IContinuationResult> IValidator<CreateUserInputPort, ICreateUserOutputPort>.ValidateAsync(
            CreateUserInputPort inputPort,
            ICreateUserOutputPort outputPort,
            CancellationToken cancellationToken)
        {
            var _HasErrors = false;
            if (!this.ValidateInputPort(inputPort, out var validationErrors))
            {
                await outputPort.PresentInvalidInputAsync(validationErrors, cancellationToken);
                _HasErrors = true;
            }

            if (!_HasErrors && inputPort.PractitionerID.HasValue && this._persistenceContext.GetEntities<Practitioner>().SingleOrDefault(p => p.ID == inputPort.PractitionerID) == null)
            {
                await outputPort.PresentPractitionerDoesNotExistAsync(inputPort.PractitionerID.Value, cancellationToken);
                _HasErrors = true;
            }

            if (!_HasErrors && this._persistenceContext.GetEntities<User>().Any(u => u.Username.Equals(inputPort.Username, StringComparison.OrdinalIgnoreCase)))
            {
                _HasErrors = true;
                await outputPort.PresentUsernameNotUniqueAsync(cancellationToken);
            }

            return new ContinuationResult(_HasErrors ? ContinuationResultBehavior.Bail : ContinuationResultBehavior.Continue);
        }

        private bool ValidateInputPort(CreateUserInputPort inputPort, out ICollection<ValidationError> errors)
        {
            errors = new List<ValidationError>();
            var _IsValid = inputPort.PractitionerID.HasValue || this._personValidator.Validate(inputPort.Person, out errors);

            if (inputPort.PractitionerID == Guid.Empty)
            {
                errors.Add(this.PropertyIsInvalid(nameof(inputPort.PractitionerID)));
                _IsValid = false;
            }

            if (string.IsNullOrWhiteSpace(inputPort.Username))
            {
                errors.Add(this.PropertyIsRequired(nameof(inputPort.Username)));
                _IsValid = false;
            }

            if (inputPort.Username.Length > 20)
            {
                errors.Add(this.PropertyIsInvalid(nameof(inputPort.Username)));
                _IsValid = false;
            }
            return _IsValid;
        }

        #endregion

    }

}
