using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Professions.UpdateProfession
{

    public class UpdateProfessionInteractor : IInteractor<UpdateProfessionInputPort, IUpdateProfessionOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Contructors

        public UpdateProfessionInteractor(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IInteractor<UpdateProfessionInputPort, IUpdateProfessionOutputPort>.InteractAsync(UpdateProfessionInputPort inputPort, IUpdateProfessionOutputPort outputPort, CancellationToken cancellationToken)
        {
            var _Profession = this._persistenceContext.GetEntities<Profession>().Single(s => s.ID == inputPort.ProfessionID);

            if (inputPort.Name.HasBeenSet)
                _Profession.Name = inputPort.Name.Value;

            await outputPort.PresentProfessionAsync(_Profession, cancellationToken);
            return new ContinuationResult();
        }

        #endregion

    }

}
