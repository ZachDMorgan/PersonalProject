using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Practitioners.UpdatePractitioner
{

    public class UpdatePractitionerInteractor : IInteractor<UpdatePractitionerInputPort, IUpdatePractitionerOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Contructors

        public UpdatePractitionerInteractor(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IInteractor<UpdatePractitionerInputPort, IUpdatePractitionerOutputPort>.InteractAsync(UpdatePractitionerInputPort inputPort, IUpdatePractitionerOutputPort outputPort, CancellationToken cancellationToken)
        {
            var _Practitioner = this._persistenceContext.GetEntities<Practitioner>()
                .Where(p => p.ID == inputPort.PractitionerID)
                .Select(p => new
                {
                    Practitioner = p,
                    p.Person,
                    p.Profession,
                    p.Services,
                }).Single().Practitioner;
            _ = this._persistenceContext.GetEntities<PractitionerService>()
                .Where(ps => ps.Practitioner.ID == inputPort.PractitionerID)
                .Select(ps => new
                {
                    PractitionerService = ps,
                    ps.Service
                });

            if (inputPort.IsActive.HasBeenSet)
                _Practitioner.IsActive = inputPort.IsActive.Value;

            if (inputPort.Person != null)
                inputPort.Person.ApplyChanges(_Practitioner.Person);

            if (inputPort.ProfessionID.HasBeenSet)
                _Practitioner.Profession = this._persistenceContext.GetEntities<Profession>().Single(p => p.ID == inputPort.ProfessionID.Value);

            if (inputPort.ServiceIDs.HasBeenSet)
            {
                var _CurrentServices = _Practitioner.Services.ToList();
                foreach (var _Service in _CurrentServices.Where(cs => !inputPort.ServiceIDs.Value.Contains(cs.Service.ID)))
                {
                    _Practitioner.Services.Remove(_Service);
                    this._persistenceContext.Remove(_Service);
                }
                foreach (var _ServiceID in inputPort.ServiceIDs.Value.Where(sid => !_Practitioner.Services.Select(s => s.Service.ID).Contains(sid)))
                    _Practitioner.Services.Add(new() { Practitioner = _Practitioner, Service = this._persistenceContext.GetEntities<Service>().Single(p => p.ID == _ServiceID) });
            }

            await outputPort.PresentPractitionerAsync(_Practitioner, cancellationToken);
            return new ContinuationResult();
        }

        #endregion

    }

}
