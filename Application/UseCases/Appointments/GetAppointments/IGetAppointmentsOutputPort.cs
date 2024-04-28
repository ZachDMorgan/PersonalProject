using Application.Dtos;
using CleanArchitecture;

namespace Application.UseCases.Appointments.GetAppointments
{

    public interface IGetAppointmentsOutputPort : IOutputPort
    {

        #region Methods

        Task PresentAppointmentsAsync(IQueryable<AppointmentDto> Appointments, CancellationToken cancellationToken);

        #endregion

    }

}
