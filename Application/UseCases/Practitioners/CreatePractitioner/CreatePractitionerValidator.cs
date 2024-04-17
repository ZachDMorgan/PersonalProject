using Application.Dtos;
using Application.DtoValidators;
using Application.Services.Persistence;
using Application.Services.Validation;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Practitioners.CreatePractitioner
{

    public class CreatePractitionerValidator : DtoValidator, IValidator<CreatePractitionerInputPort, ICreatePractitionerOutputPort>
    {

        #region Fields

        private readonly IDtoValidator<ContactDetailsDto> _contactDetailsValidator;
        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Constructors

        public CreatePractitionerValidator(IPersistenceContext persistenceContext)
        {
            this._contactDetailsValidator = new ContactDetailsValidator();
            this._persistenceContext = persistenceContext;
        }

        #endregion

        #region Methods

        async Task<IContinuationResult> IValidator<CreatePractitionerInputPort, ICreatePractitionerOutputPort>.ValidateAsync(
            CreatePractitionerInputPort inputPort,
            ICreatePractitionerOutputPort outputPort,
            CancellationToken cancellationToken)
        {
            var _HasErrors = false;
            if (!this.ValidateInputPort(inputPort, out var validationErrors))
            {
                await outputPort.PresentInvalidInputAsync(validationErrors, cancellationToken);
                _HasErrors = true;
            }

            if (!_HasErrors && this._persistenceContext.GetEntities<Profession>().SingleOrDefault(p => p.ID == inputPort.ProfessionID) == null)
            {
                await outputPort.PresentProfessionDoesNotExistAsync(inputPort.ProfessionID, cancellationToken);
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

            if (!_HasErrors && this._persistenceContext.GetEntities<Practitioner>()
                .Any(p => p.FirstName.Equals(inputPort.FirstName, StringComparison.OrdinalIgnoreCase)
                    && p.Surname.Equals(inputPort.Surname, StringComparison.OrdinalIgnoreCase)
                    && p.Title.Equals(inputPort.Title, StringComparison.OrdinalIgnoreCase)))
                return await outputPort.PresentPractitionerNameNotUniqueAsync(cancellationToken);

            return new ContinuationResult(_HasErrors ? ContinuationResultBehavior.Bail : ContinuationResultBehavior.Continue);
        }

        private bool ValidateInputPort(CreatePractitionerInputPort inputPort, out ICollection<ValidationError> errors)
        {
            var _IsValid = this._contactDetailsValidator.Validate(inputPort.ContactDetails, out errors);

            if (string.IsNullOrWhiteSpace(inputPort.FirstName))
            {
                errors.Add(this.PropertyIsRequired(nameof(inputPort.FirstName)));
                _IsValid = false;
            }

            if (inputPort.ProfessionID == Guid.Empty)
            {
                errors.Add(this.PropertyIsInvalid(nameof(inputPort.ProfessionID)));
                _IsValid = false;
            }

            if (!(inputPort.ServiceIDs?.Any() ?? true))
            {
                errors.Add(this.PropertyIsEmpty(nameof(inputPort.ServiceIDs)));
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
            return _IsValid;
        }

        #endregion

    }

}
