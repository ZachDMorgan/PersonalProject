using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Services.CreateService
{

    public class CreateServiceInteractor : IInteractor<CreateServiceInputPort, ICreateServiceOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Contructors

        public CreateServiceInteractor(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IInteractor<CreateServiceInputPort, ICreateServiceOutputPort>.InteractAsync(CreateServiceInputPort inputPort, ICreateServiceOutputPort outputPort, CancellationToken cancellationToken)
        {
            var _Service = new Service()
            {
                Cost = inputPort.Cost,
                Duration = inputPort.Duration,
                Name = inputPort.Name,
            };

            this._persistenceContext.Add(_Service);

            await outputPort.PresentServiceAsync(_Service, cancellationToken);
            return new ContinuationResult();
        }

        #endregion

    }

}
