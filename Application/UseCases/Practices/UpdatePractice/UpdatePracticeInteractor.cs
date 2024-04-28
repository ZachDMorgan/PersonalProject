using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Practices.UpdatePractice
{

    public class UpdatePracticeInteractor : IInteractor<UpdatePracticeInputPort, IUpdatePracticeOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Contructors

        public UpdatePracticeInteractor(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IInteractor<UpdatePracticeInputPort, IUpdatePracticeOutputPort>.InteractAsync(UpdatePracticeInputPort inputPort, IUpdatePracticeOutputPort outputPort, CancellationToken cancellationToken)
        {
            var _Practice = this._persistenceContext
                .GetEntities<Practice>()
                .Where(p => p.ID == inputPort.PracticeID)
                .Select(p => new
                {
                    Practice = p,
                    p.ContactDetails
                }).Single().Practice;

            if (inputPort.Address.HasBeenSet)
                _Practice.Address = inputPort.Address.Value;

            if (inputPort.ContactDetails != null)
                inputPort.ContactDetails.ApplyChanges(_Practice.ContactDetails);

            if (inputPort.Description.HasBeenSet)
                _Practice.Description = inputPort.Description.Value;

            if (inputPort.IsActive.HasBeenSet)
                _Practice.IsActive = inputPort.IsActive.Value;

            if (inputPort.Name.HasBeenSet)
                _Practice.Name = inputPort.Name.Value;

            await outputPort.PresentPracticeAsync(_Practice, cancellationToken);
            return new ContinuationResult();
        }

        #endregion

    }

}
