using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Practitioners.DeletePractitioner
{

    public class DeletePractitionerInteractor : IInteractor<DeletePractitionerInputPort, IDeletePractitionerOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Contructors

        public DeletePractitionerInteractor(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IInteractor<DeletePractitionerInputPort, IDeletePractitionerOutputPort>.InteractAsync(DeletePractitionerInputPort inputPort, IDeletePractitionerOutputPort outputPort, CancellationToken cancellationToken)
        {
            var _Practitioner = this._persistenceContext.GetEntities<Practitioner>().Single(s => s.ID == inputPort.PractitionerID);
            this._persistenceContext.Remove(_Practitioner);
            await outputPort.PresentDeletedPractitionerAsync(_Practitioner, cancellationToken);
            return new ContinuationResult();
        }

        #endregion

    }

}
