using Application.Dtos;
using Application.DtoValidators;
using Application.Services.Persistence;
using Application.Services.Validation;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Practitioners.UpdatePractitioner
{

    public class UpdatePractitionerValidator : DtoValidator, IValidator<UpdatePractitionerInputPort, IUpdatePractitionerOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;
        private readonly IDtoValidator<UpdatePersonDto> _personValidator;

        #endregion

        #region Constructors

        public UpdatePractitionerValidator(IPersistenceContext persistenceContext)
        {
            this._persistenceContext = persistenceContext;
            this._personValidator = new UpdatePersonValidator(new UpdateContactDetailsValidator());
        }

        #endregion

        #region Methods

        async Task<IContinuationResult> IValidator<UpdatePractitionerInputPort, IUpdatePractitionerOutputPort>.ValidateAsync(
            UpdatePractitionerInputPort inputPort,
            IUpdatePractitionerOutputPort outputPort,
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
                await outputPort.PresentPractitionerNotFoundAsync(inputPort.PractitionerID, cancellationToken);
                _HasErrors = true;
            }

            if (!_HasErrors
                && inputPort.ProfessionID.HasBeenSet
                && this._persistenceContext.GetEntities<Profession>().SingleOrDefault(p => p.ID != inputPort.ProfessionID.Value) == null)
            {
                await outputPort.PresentProfessionNotFoundAsync(inputPort.ProfessionID.Value, cancellationToken);
                _HasErrors = true;
            }

            if (!_HasErrors && inputPort.ServiceIDs.HasBeenSet)
            {
                var _InvalidIDs = inputPort.ServiceIDs.Value.Except(this._persistenceContext.GetEntities<Service>().Where(s => inputPort.ServiceIDs.Value.Contains(s.ID)).Select(s => s.ID)).ToList();
                if (_InvalidIDs.Any())
                {
                    await outputPort.PresentServicesNotFoundAsync(_InvalidIDs, cancellationToken);
                    _HasErrors = true;
                }
            }

            return new ContinuationResult(_HasErrors ? ContinuationResultBehavior.Bail : ContinuationResultBehavior.Continue);
        }

        private bool ValidateInputPort(UpdatePractitionerInputPort inputPort, out ICollection<ValidationError> errors)
        {
            errors = new List<ValidationError>();
            var _IsValid = this._personValidator.Validate(inputPort.Person, out errors);

            if (inputPort.PractitionerID == Guid.Empty)
            {
                errors.Add(this.PropertyIsInvalid(nameof(inputPort.PractitionerID)));
                _IsValid = false;
            }

            if (inputPort.ProfessionID.HasBeenSet && inputPort.ProfessionID.Value == Guid.Empty)
            {
                errors.Add(this.PropertyIsInvalid(nameof(inputPort.ProfessionID)));
                _IsValid = false;
            }

            if (inputPort.ServiceIDs.HasBeenSet && !(inputPort.ServiceIDs.Value?.Any() ?? true))
            {
                errors.Add(this.PropertyIsEmpty(nameof(inputPort.ServiceIDs)));
                _IsValid = false;
            }
            return _IsValid;
        }

        #endregion

    }

}
