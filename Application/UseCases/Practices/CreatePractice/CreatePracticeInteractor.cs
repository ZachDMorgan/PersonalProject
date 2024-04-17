using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Practices.CreatePractice
{

    public class CreatePracticeInteractor : IInteractor<CreatePracticeInputPort, ICreatePracticeOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Contructors

        public CreatePracticeInteractor(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IInteractor<CreatePracticeInputPort, ICreatePracticeOutputPort>.InteractAsync(CreatePracticeInputPort inputPort, ICreatePracticeOutputPort outputPort, CancellationToken cancellationToken)
        {
            var _Practice = new Practice()
            {
                Address = inputPort.Address,
                ContactDetails = (ContactDetails)inputPort.ContactDetails,
                Description = inputPort.Description,
                Name = inputPort.Name,
            };

            this._persistenceContext.Add(_Practice);

            await outputPort.PresentPracticeAsync(_Practice, cancellationToken);
            return new ContinuationResult();
        }

        #endregion

    }

}
