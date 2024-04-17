using Application.Dtos;
using Application.DtoValidators;
using Application.Services.Persistence;
using Application.Services.Validation;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Practices.CreatePractice
{

    public class CreatePracticeValidator : DtoValidator, IValidator<CreatePracticeInputPort, ICreatePracticeOutputPort>
    {

        #region Fields

        private readonly IDtoValidator<ContactDetailsDto> _contactDetailsValidator;
        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Constructors

        public CreatePracticeValidator(IPersistenceContext persistenceContext)
        {
            this._contactDetailsValidator = new ContactDetailsValidator();
            this._persistenceContext = persistenceContext;
        }

        #endregion

        #region Methods

        async Task<IContinuationResult> IValidator<CreatePracticeInputPort, ICreatePracticeOutputPort>.ValidateAsync(
            CreatePracticeInputPort inputPort,
            ICreatePracticeOutputPort outputPort,
            CancellationToken cancellationToken)
        {
            var _HasErrors = false;
            if (!this.ValidateInputPort(inputPort, out var validationErrors))
            {
                await outputPort.PresentInvalidInputAsync(validationErrors, cancellationToken);
                _HasErrors = true;
            }

            if (!_HasErrors && this._persistenceContext.GetEntities<Practice>().Any(p => p.Name.Equals(inputPort.Name, StringComparison.OrdinalIgnoreCase)))
            {
                await outputPort.PresentPracticeNameNotUniqueAsync(cancellationToken);
                _HasErrors = true;
            }

            return new ContinuationResult(_HasErrors ? ContinuationResultBehavior.Bail : ContinuationResultBehavior.Continue);
        }

        private bool ValidateInputPort(CreatePracticeInputPort inputPort, out ICollection<ValidationError> errors)
        {
            var _IsValid = this._contactDetailsValidator.Validate(inputPort.ContactDetails, out errors);

            if (string.IsNullOrWhiteSpace(inputPort.Address))
            {
                errors.Add(this.PropertyIsRequired(nameof(inputPort.Address)));
                _IsValid = false;
            }

            if (string.IsNullOrWhiteSpace(inputPort.Name))
            {
                errors.Add(this.PropertyIsRequired(nameof(inputPort.Name)));
                _IsValid = false;
            }
            return _IsValid;
        }

        #endregion

    }

}
