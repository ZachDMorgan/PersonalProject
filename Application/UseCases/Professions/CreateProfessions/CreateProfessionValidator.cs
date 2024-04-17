using Application.DtoValidators;
using Application.Services.Persistence;
using Application.Services.Validation;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Professions.CreateProfession
{

    public class CreateProfessionValidator : DtoValidator, IValidator<CreateProfessionInputPort, ICreateProfessionOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Constructors

        public CreateProfessionValidator(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IValidator<CreateProfessionInputPort, ICreateProfessionOutputPort>.ValidateAsync(
            CreateProfessionInputPort inputPort,
            ICreateProfessionOutputPort outputPort,
            CancellationToken cancellationToken)
        {
            var _HasErrors = false;
            if (string.IsNullOrWhiteSpace(inputPort.Name))
            {
                await outputPort.PresentInvalidInputAsync(new List<ValidationError>() { this.PropertyIsRequired(nameof(inputPort.Name)) }, cancellationToken);
                _HasErrors = true;
            }

            if (!_HasErrors && this._persistenceContext.GetEntities<Profession>().Any(p => p.Name.Equals(inputPort.Name, StringComparison.OrdinalIgnoreCase)))
            {
                await outputPort.PresentProfessionNameNotUniqueAsync(cancellationToken);
                _HasErrors = true;
            }

            return new ContinuationResult(_HasErrors ? ContinuationResultBehavior.Bail : ContinuationResultBehavior.Continue);
        }

        #endregion

    }

}
