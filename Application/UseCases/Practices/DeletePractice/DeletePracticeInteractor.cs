using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Practices.DeletePractice
{

    public class DeletePracticeInteractor : IInteractor<DeletePracticeInputPort, IDeletePracticeOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Contructors

        public DeletePracticeInteractor(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IInteractor<DeletePracticeInputPort, IDeletePracticeOutputPort>.InteractAsync(DeletePracticeInputPort inputPort, IDeletePracticeOutputPort outputPort, CancellationToken cancellationToken)
        {
            var _Practice = this._persistenceContext.GetEntities<Practice>().Single(s => s.ID == inputPort.PracticeID);
            this._persistenceContext.Remove(_Practice);
            await outputPort.PresentDeletedPracticeAsync(_Practice, cancellationToken);
            return new ContinuationResult();
        }

        #endregion

    }

}
