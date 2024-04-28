using Application.Dtos;
using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Services.GetServices
{

    public class GetServicesInteractor : IInteractor<GetServicesInputPort, IGetServicesOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Contructors

        public GetServicesInteractor(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IInteractor<GetServicesInputPort, IGetServicesOutputPort>.InteractAsync(GetServicesInputPort inputPort, IGetServicesOutputPort outputPort, CancellationToken cancellationToken)
        {
            await outputPort.PresentServicesAsync(this._persistenceContext.GetEntities<Service>().Select(s => (ServiceDto)s), cancellationToken);
            return new ContinuationResult();
        }

        #endregion

    }

}
