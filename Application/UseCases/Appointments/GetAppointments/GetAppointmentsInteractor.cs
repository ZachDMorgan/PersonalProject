using Application.Dtos;
using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Appointments.GetAppointments
{

    public class GetAppointmentsInteractor : IInteractor<GetAppointmentsInputPort, IGetAppointmentsOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Contructors

        public GetAppointmentsInteractor(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IInteractor<GetAppointmentsInputPort, IGetAppointmentsOutputPort>.InteractAsync(GetAppointmentsInputPort inputPort, IGetAppointmentsOutputPort outputPort, CancellationToken cancellationToken)
        {
            await outputPort.PresentAppointmentsAsync(this._persistenceContext.GetEntities<Appointment>().Select(s => (AppointmentDto)s), cancellationToken);
            return new ContinuationResult();
        }

        #endregion

    }

}
