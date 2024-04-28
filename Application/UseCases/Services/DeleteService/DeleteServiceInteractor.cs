using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Services.DeleteService
{

    public class DeleteServiceInteractor : IInteractor<DeleteServiceInputPort, IDeleteServiceOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Contructors

        public DeleteServiceInteractor(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IInteractor<DeleteServiceInputPort, IDeleteServiceOutputPort>.InteractAsync(DeleteServiceInputPort inputPort, IDeleteServiceOutputPort outputPort, CancellationToken cancellationToken)
        {
            var _Service = this._persistenceContext.GetEntities<Service>().Single(s => s.ID == inputPort.ServiceID);
            this._persistenceContext.Remove(_Service);
            await outputPort.PresentDeletedServiceAsync(_Service, cancellationToken);
            return new ContinuationResult();
        }

        #endregion

    }

}
