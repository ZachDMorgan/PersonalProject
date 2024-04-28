using Application.DtoValidators;
using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Services.DeleteService
{

    public class DeleteServiceValidator : DtoValidator, IValidator<DeleteServiceInputPort, IDeleteServiceOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Constructors

        public DeleteServiceValidator(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IValidator<DeleteServiceInputPort, IDeleteServiceOutputPort>.ValidateAsync(
            DeleteServiceInputPort inputPort,
            IDeleteServiceOutputPort outputPort,
            CancellationToken cancellationToken)
        {
            if (this._persistenceContext.GetEntities<Service>().SingleOrDefault(s => s.ID == inputPort.ServiceID) == null)
            {
                await outputPort.PresentServiceNotFoundAsync(inputPort.ServiceID, cancellationToken);
                return new ContinuationResult(ContinuationResultBehavior.Bail);
            }

            if (this._persistenceContext.GetEntities<Appointment>().Any(a => a.Service.ID == inputPort.ServiceID)
                || this._persistenceContext.GetEntities<PractitionerService>().Any(ps => ps.Service.ID == inputPort.ServiceID))
            {
                await outputPort.PresentServiceInUseAsync(cancellationToken);
                return new ContinuationResult(ContinuationResultBehavior.Bail);
            }

            return new ContinuationResult(ContinuationResultBehavior.Continue);
        }

        #endregion

    }

}
