using Application.Dtos;
using Application.DtoValidators;
using Application.Services.Persistence;
using Application.Services.Validation;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Practices.UpdatePractice
{

    public class UpdatePracticeValidator : DtoValidator, IValidator<UpdatePracticeInputPort, IUpdatePracticeOutputPort>
    {

        #region Fields

        private readonly IDtoValidator<UpdateContactDetailsDto> _contactDetailsValidator;
        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Constructors

        public UpdatePracticeValidator(UpdateContactDetailsValidator contactDetailsValidator, IPersistenceContext persistenceContext)
        {
            this._contactDetailsValidator = contactDetailsValidator;
            this._persistenceContext = persistenceContext;
        }

        #endregion

        #region Methods

        async Task<IContinuationResult> IValidator<UpdatePracticeInputPort, IUpdatePracticeOutputPort>.ValidateAsync(
            UpdatePracticeInputPort inputPort,
            IUpdatePracticeOutputPort outputPort,
            CancellationToken cancellationToken)
        {
            var _HasErrors = false;
            if (!this.ValidateInputPort(inputPort, out var validationErrors))
            {
                await outputPort.PresentInvalidInputAsync(validationErrors, cancellationToken);
                _HasErrors = true;
            }

            if (!_HasErrors && this._persistenceContext.GetEntities<Practice>().SingleOrDefault(p => p.ID == inputPort.PracticeID) == null)
            {
                await outputPort.PresentPracticeNotFoundAsync(inputPort.PracticeID, cancellationToken);
                _HasErrors = true;
            }

            if (!_HasErrors
                && inputPort.Name.HasBeenSet
                && this._persistenceContext.GetEntities<Practice>().Where(p => p.ID != inputPort.PracticeID).Any(p => p.Name.Equals(inputPort.Name.Value, StringComparison.OrdinalIgnoreCase)))
            {
                await outputPort.PresentPracticeNameNotUniqueAsync(cancellationToken);
                _HasErrors = true;
            }

            return new ContinuationResult(_HasErrors ? ContinuationResultBehavior.Bail : ContinuationResultBehavior.Continue);
        }

        private bool ValidateInputPort(UpdatePracticeInputPort inputPort, out ICollection<ValidationError> errors)
        {
            errors = new List<ValidationError>();
            var _IsValid = this._contactDetailsValidator.Validate(inputPort.ContactDetails, out errors);

            if (inputPort.PracticeID == Guid.Empty)
            {
                errors.Add(this.PropertyIsInvalid(nameof(inputPort.PracticeID)));
                _IsValid = false;
            }

            if (inputPort.Address.HasBeenSet && string.IsNullOrWhiteSpace(inputPort.Address.Value))
            {
                errors.Add(this.PropertyIsRequired(nameof(inputPort.Address)));
                _IsValid = false;
            }

            if (inputPort.Name.HasBeenSet && string.IsNullOrWhiteSpace(inputPort.Name.Value))
            {
                errors.Add(this.PropertyIsRequired(nameof(inputPort.Name)));
                _IsValid = false;
            }
            return _IsValid;
        }

        #endregion

    }

}
