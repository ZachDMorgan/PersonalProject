using Application.Dtos;
using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Practitioners.GetPractitioners
{

    public class GetPractitionersInteractor : IInteractor<GetPractitionersInputPort, IGetPractitionersOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Contructors

        public GetPractitionersInteractor(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IInteractor<GetPractitionersInputPort, IGetPractitionersOutputPort>.InteractAsync(GetPractitionersInputPort inputPort, IGetPractitionersOutputPort outputPort, CancellationToken cancellationToken)
        {
            await outputPort.PresentPractitionersAsync(this._persistenceContext.GetEntities<Practitioner>().Select(s => (PractitionerDto)s), cancellationToken);
            return new ContinuationResult();
        }

        #endregion

    }

}
