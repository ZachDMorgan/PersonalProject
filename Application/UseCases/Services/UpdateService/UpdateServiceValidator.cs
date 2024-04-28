using Application.DtoValidators;
using Application.Services.Persistence;
using Application.Services.Validation;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Services.UpdateService
{

    public class UpdateServiceValidator : DtoValidator, IValidator<UpdateServiceInputPort, IUpdateServiceOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Constructors

        public UpdateServiceValidator(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IValidator<UpdateServiceInputPort, IUpdateServiceOutputPort>.ValidateAsync(
            UpdateServiceInputPort inputPort,
            IUpdateServiceOutputPort outputPort,
            CancellationToken cancellationToken)
        {
            var _HasErrors = false;
            if (!this.ValidateInputPort(inputPort, out var validationErrors))
            {
                await outputPort.PresentInvalidInputAsync(validationErrors, cancellationToken);
                _HasErrors = true;
            }

            if (!_HasErrors && this._persistenceContext.GetEntities<Service>().SingleOrDefault(s => s.ID == inputPort.ServiceID) == null)
            {
                await outputPort.PresentServiceNotFoundAsync(inputPort.ServiceID, cancellationToken);
                _HasErrors = true;
            }

            if (!_HasErrors
                && inputPort.Name.HasBeenSet
                && this._persistenceContext.GetEntities<Service>().Where(s => s.ID != inputPort.ServiceID).Any(p => p.Name.Equals(inputPort.Name.Value, StringComparison.OrdinalIgnoreCase)))
            {
                await outputPort.PresentServiceNameNotUniqueAsync(cancellationToken);
                _HasErrors = true;
            }

            return new ContinuationResult(_HasErrors ? ContinuationResultBehavior.Bail : ContinuationResultBehavior.Continue);
        }

        private bool ValidateInputPort(UpdateServiceInputPort inputPort, out ICollection<ValidationError> errors)
        {
            var _IsValid = true;
            errors = new List<ValidationError>();

            if (inputPort.ServiceID == Guid.Empty)
            {
                errors.Add(this.PropertyIsInvalid(nameof(inputPort.ServiceID)));
                _IsValid = false;
            }

            if (inputPort.Cost.HasBeenSet && inputPort.Cost.Value < 0)
            {
                errors.Add(this.PropertyIsLessThanZero(nameof(inputPort.Cost)));
                _IsValid = false;
            }

            if (inputPort.Duration.HasBeenSet && inputPort.Duration.Value < 0)
            {
                errors.Add(this.PropertyIsLessThanZero(nameof(inputPort.Duration)));
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
