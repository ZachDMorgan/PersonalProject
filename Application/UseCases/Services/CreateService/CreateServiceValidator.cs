using Application.DtoValidators;
using Application.Services.Persistence;
using Application.Services.Validation;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Services.CreateService
{

    public class CreateServiceValidator : DtoValidator, IValidator<CreateServiceInputPort, ICreateServiceOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Constructors

        public CreateServiceValidator(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IValidator<CreateServiceInputPort, ICreateServiceOutputPort>.ValidateAsync(
            CreateServiceInputPort inputPort,
            ICreateServiceOutputPort outputPort,
            CancellationToken cancellationToken)
        {
            var _HasErrors = false;
            if (!this.ValidateInputPort(inputPort, out var validationErrors))
            {
                await outputPort.PresentInvalidInputAsync(validationErrors, cancellationToken);
                _HasErrors = true;
            }

            if (!_HasErrors && this._persistenceContext.GetEntities<Service>().Any(p => p.Name.Equals(inputPort.Name, StringComparison.OrdinalIgnoreCase)))
            {
                await outputPort.PresentServiceNameNotUniqueAsync(cancellationToken);
                _HasErrors = true;
            }

            return new ContinuationResult(_HasErrors ? ContinuationResultBehavior.Bail : ContinuationResultBehavior.Continue);
        }

        private bool ValidateInputPort(CreateServiceInputPort inputPort, out ICollection<ValidationError> errors)
        {
            var _IsValid = true;
            errors = new List<ValidationError>();

            if (inputPort.Cost < 0)
            {
                errors.Add(this.PropertyIsLessThanZero(nameof(inputPort.Cost)));
                _IsValid = false;
            }

            if (inputPort.Duration < 0)
            {
                errors.Add(this.PropertyIsLessThanZero(nameof(inputPort.Duration)));
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
