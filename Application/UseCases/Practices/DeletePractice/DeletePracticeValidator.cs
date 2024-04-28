using Application.DtoValidators;
using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Practices.DeletePractice
{

    public class DeletePracticeValidator : DtoValidator, IValidator<DeletePracticeInputPort, IDeletePracticeOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Constructors

        public DeletePracticeValidator(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IValidator<DeletePracticeInputPort, IDeletePracticeOutputPort>.ValidateAsync(
            DeletePracticeInputPort inputPort,
            IDeletePracticeOutputPort outputPort,
            CancellationToken cancellationToken)
        {
            if (this._persistenceContext.GetEntities<Practice>().SingleOrDefault(s => s.ID == inputPort.PracticeID) == null)
            {
                await outputPort.PresentPracticeNotFoundAsync(inputPort.PracticeID, cancellationToken);
                return new ContinuationResult(ContinuationResultBehavior.Bail);
            }

            return new ContinuationResult(ContinuationResultBehavior.Continue);
        }

        #endregion

    }

}
