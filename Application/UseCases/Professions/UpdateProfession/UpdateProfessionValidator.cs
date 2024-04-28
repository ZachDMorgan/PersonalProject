using Application.DtoValidators;
using Application.Services.Persistence;
using Application.Services.Validation;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Professions.UpdateProfession
{

    public class UpdateProfessionValidator : DtoValidator, IValidator<UpdateProfessionInputPort, IUpdateProfessionOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Constructors

        public UpdateProfessionValidator(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IValidator<UpdateProfessionInputPort, IUpdateProfessionOutputPort>.ValidateAsync(
            UpdateProfessionInputPort inputPort,
            IUpdateProfessionOutputPort outputPort,
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
                await outputPort.PresentProfessionNotFoundAsync(inputPort.ProfessionID, cancellationToken);
                _HasErrors = true;
            }

            if (!_HasErrors
                && inputPort.Name.HasBeenSet
                && this._persistenceContext.GetEntities<Profession>().Where(p => p.ID != inputPort.ProfessionID).Any(p => p.Name.Equals(inputPort.Name.Value, StringComparison.OrdinalIgnoreCase)))
            {
                await outputPort.PresentProfessionNameNotUniqueAsync(cancellationToken);
                _HasErrors = true;
            }

            return new ContinuationResult(_HasErrors ? ContinuationResultBehavior.Bail : ContinuationResultBehavior.Continue);
        }

        private bool ValidateInputPort(UpdateProfessionInputPort inputPort, out ICollection<ValidationError> errors)
        {
            var _IsValid = true;
            errors = new List<ValidationError>();

            if (inputPort.ProfessionID == Guid.Empty)
            {
                errors.Add(this.PropertyIsInvalid(nameof(inputPort.ProfessionID)));
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
