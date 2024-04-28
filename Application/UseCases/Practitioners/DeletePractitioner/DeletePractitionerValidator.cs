using Application.DtoValidators;
using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Practitioners.DeletePractitioner
{

    public class DeletePractitionerValidator : DtoValidator, IValidator<DeletePractitionerInputPort, IDeletePractitionerOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Constructors

        public DeletePractitionerValidator(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IValidator<DeletePractitionerInputPort, IDeletePractitionerOutputPort>.ValidateAsync(
            DeletePractitionerInputPort inputPort,
            IDeletePractitionerOutputPort outputPort,
            CancellationToken cancellationToken)
        {
            if (this._persistenceContext.GetEntities<Practitioner>().SingleOrDefault(s => s.ID == inputPort.PractitionerID) == null)
            {
                await outputPort.PresentPractitionerNotFoundAsync(inputPort.PractitionerID, cancellationToken);
                return new ContinuationResult(ContinuationResultBehavior.Bail);
            }

            if (this._persistenceContext.GetEntities<Appointment>().Any(a => a.Practitioner.ID == inputPort.PractitionerID))
            {
                await outputPort.PresentPractitionerInUseAsync(cancellationToken);
                return new ContinuationResult(ContinuationResultBehavior.Bail);
            }

            return new ContinuationResult(ContinuationResultBehavior.Continue);
        }

        #endregion

    }

}
