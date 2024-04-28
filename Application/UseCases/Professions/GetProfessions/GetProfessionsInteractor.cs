using Application.Dtos;
using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Professions.GetProfessions
{

    public class GetProfessionsInteractor : IInteractor<GetProfessionsInputPort, IGetProfessionsOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Contructors

        public GetProfessionsInteractor(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IInteractor<GetProfessionsInputPort, IGetProfessionsOutputPort>.InteractAsync(GetProfessionsInputPort inputPort, IGetProfessionsOutputPort outputPort, CancellationToken cancellationToken)
        {
            await outputPort.PresentProfessionsAsync(this._persistenceContext.GetEntities<Profession>().Select(s => (ProfessionDto)s), cancellationToken);
            return new ContinuationResult();
        }

        #endregion

    }

}
