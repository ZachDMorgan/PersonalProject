using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Practitioners.CreatePractitioner
{

    public class CreatePractitionerInteractor : IInteractor<CreatePractitionerInputPort, ICreatePractitionerOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Contructors

        public CreatePractitionerInteractor(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IInteractor<CreatePractitionerInputPort, ICreatePractitionerOutputPort>.InteractAsync(CreatePractitionerInputPort inputPort, ICreatePractitionerOutputPort outputPort, CancellationToken cancellationToken)
        {
            var _Profession = this._persistenceContext.GetEntities<Profession>().Single(p => p.ID == inputPort.ProfessionID);
            var _Services = this._persistenceContext.GetEntities<Service>().Where(s => inputPort.ServiceIDs.Contains(s.ID)).ToList();
            var _Practitioner = new Practitioner()
            {
                IsActive = true,
                Person = inputPort.Person,
                Profession = _Profession,
            };
            _Practitioner.Services = _Services.Select(s => new PractitionerService() { Practitioner = _Practitioner, Service = s }).ToHashSet();

            this._persistenceContext.Add(_Practitioner);

            await outputPort.PresentPractitionerAsync(_Practitioner, cancellationToken);
            return new ContinuationResult();
        }

        #endregion

    }

}
