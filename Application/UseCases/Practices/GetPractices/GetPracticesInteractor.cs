using Application.Dtos;
using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Practices.GetPractices
{

    public class GetPracticesInteractor : IInteractor<GetPracticesInputPort, IGetPracticesOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Contructors

        public GetPracticesInteractor(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IInteractor<GetPracticesInputPort, IGetPracticesOutputPort>.InteractAsync(GetPracticesInputPort inputPort, IGetPracticesOutputPort outputPort, CancellationToken cancellationToken)
        {
            await outputPort.PresentPracticesAsync(this._persistenceContext.GetEntities<Practice>().Select(s => (PracticeDto)s), cancellationToken);
            return new ContinuationResult();
        }

        #endregion

    }

}
