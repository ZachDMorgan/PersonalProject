using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Professions.CreateProfession
{

    public class CreateProfessionInteractor : IInteractor<CreateProfessionInputPort, ICreateProfessionOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Contructors

        public CreateProfessionInteractor(IPersistenceContext persistenceContext)
            => _persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IInteractor<CreateProfessionInputPort, ICreateProfessionOutputPort>.InteractAsync(CreateProfessionInputPort inputPort, ICreateProfessionOutputPort outputPort, CancellationToken cancellationToken)
        {
            var _Profession = new Profession()
            {
                Name = inputPort.Name,
            };

            _persistenceContext.Add(_Profession);

            await outputPort.PresentProfessionAsync(_Profession, cancellationToken);
            return new ContinuationResult();
        }

        #endregion

    }

}
