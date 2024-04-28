using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Services.UpdateService
{

    public class UpdateServiceInteractor : IInteractor<UpdateServiceInputPort, IUpdateServiceOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Contructors

        public UpdateServiceInteractor(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IInteractor<UpdateServiceInputPort, IUpdateServiceOutputPort>.InteractAsync(UpdateServiceInputPort inputPort, IUpdateServiceOutputPort outputPort, CancellationToken cancellationToken)
        {
            var _Service = this._persistenceContext.GetEntities<Service>().Single(s => s.ID == inputPort.ServiceID);

            if (inputPort.Cost.HasBeenSet)
                _Service.Cost = inputPort.Cost.Value;

            if (inputPort.Duration.HasBeenSet)
                _Service.Duration = inputPort.Duration.Value;

            if (inputPort.Name.HasBeenSet)
                _Service.Name = inputPort.Name.Value;

            await outputPort.PresentServiceAsync(_Service, cancellationToken);
            return new ContinuationResult();
        }

        #endregion

    }

}
