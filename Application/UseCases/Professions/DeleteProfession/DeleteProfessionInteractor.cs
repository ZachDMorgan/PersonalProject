using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Professions.DeleteProfession
{

    public class DeleteProfessionInteractor : IInteractor<DeleteProfessionInputPort, IDeleteProfessionOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Contructors

        public DeleteProfessionInteractor(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IInteractor<DeleteProfessionInputPort, IDeleteProfessionOutputPort>.InteractAsync(DeleteProfessionInputPort inputPort, IDeleteProfessionOutputPort outputPort, CancellationToken cancellationToken)
        {
            var _Profession = this._persistenceContext.GetEntities<Profession>().Single(s => s.ID == inputPort.ProfessionID);
            this._persistenceContext.Remove(_Profession);
            await outputPort.PresentDeletedProfessionAsync(_Profession, cancellationToken);
            return new ContinuationResult();
        }

        #endregion

    }

}
