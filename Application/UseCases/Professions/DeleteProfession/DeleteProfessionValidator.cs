using Application.DtoValidators;
using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Professions.DeleteProfession
{

    public class DeleteProfessionValidator : DtoValidator, IValidator<DeleteProfessionInputPort, IDeleteProfessionOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Constructors

        public DeleteProfessionValidator(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IValidator<DeleteProfessionInputPort, IDeleteProfessionOutputPort>.ValidateAsync(
            DeleteProfessionInputPort inputPort,
            IDeleteProfessionOutputPort outputPort,
            CancellationToken cancellationToken)
        {
            if (this._persistenceContext.GetEntities<Profession>().SingleOrDefault(s => s.ID == inputPort.ProfessionID) == null)
            {
                await outputPort.PresentProfessionNotFoundAsync(inputPort.ProfessionID, cancellationToken);
                return new ContinuationResult(ContinuationResultBehavior.Bail);
            }

            if (this._persistenceContext.GetEntities<Practitioner>().Any(p => p.Profession.ID == inputPort.ProfessionID))
            {
                await outputPort.PresentProfessionInUseAsync(cancellationToken);
                return new ContinuationResult(ContinuationResultBehavior.Bail);
            }

            return new ContinuationResult(ContinuationResultBehavior.Continue);
        }

        #endregion

    }

}
