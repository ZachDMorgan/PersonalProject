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

        private readonly IDtoValidator<ContactDetailsDto> _contactDetailsValidator;
        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Constructors

        public CreateUserValidator(IPersistenceContext persistenceContext)
        {
            this._contactDetailsValidator = new ContactDetailsValidator();
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

            if (!_HasErrors && this._persistenceContext.GetEntities<Practitioner>().SingleOrDefault(p => p.ID == inputPort.PractitionerID) == null)
            {
                await outputPort.PresentPractitionerDoesNotExistAsync(inputPort.PractitionerID, cancellationToken);
                _HasErrors = true;
            }

            if (!_HasErrors)
            {
                var _Services = this._persistenceContext.GetEntities<Service>().Where(s => inputPort.ServiceIDs.Contains(s.ID)).ToList();
                var _InvalidIDs = inputPort.ServiceIDs.Except(_Services.Select(s => s.ID)).ToList();
                if (_InvalidIDs.Any())
                {
                    await outputPort.PresentServicesDoNotExistAsync(_InvalidIDs, cancellationToken);
                    _HasErrors = true;
                }
            }

            if (!_HasErrors && this._persistenceContext.GetEntities<User>()
                .Any(p => p.FirstName.Equals(inputPort.FirstName, StringComparison.OrdinalIgnoreCase)
                    && p.Surname.Equals(inputPort.Surname, StringComparison.OrdinalIgnoreCase)
                    && p.Title.Equals(inputPort.Title, StringComparison.OrdinalIgnoreCase)))
                return await outputPort.PresentUserNameNotUniqueAsync(cancellationToken);

            return new ContinuationResult(_HasErrors ? ContinuationResultBehavior.Bail : ContinuationResultBehavior.Continue);
        }

        private bool ValidateInputPort(CreateUserInputPort inputPort, out ICollection<ValidationError> errors)
        {
            var _IsValid = this._contactDetailsValidator.Validate(inputPort.ContactDetails, out errors);

            if (string.IsNullOrWhiteSpace(inputPort.FirstName))
            {
                errors.Add(this.PropertyIsRequired(nameof(inputPort.FirstName)));
                _IsValid = false;
            }

            if (inputPort.PractitionerID == Guid.Empty)
            {
                errors.Add(this.PropertyIsInvalid(nameof(inputPort.PractitionerID)));
                _IsValid = false;
            }

            if (string.IsNullOrWhiteSpace(inputPort.Surname))
            {
                errors.Add(this.PropertyIsRequired(nameof(inputPort.Surname)));
                _IsValid = false;
            }

            if (string.IsNullOrWhiteSpace(inputPort.Title))
            {
                errors.Add(this.PropertyIsRequired(nameof(inputPort.Title)));
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
