using Application.Dtos;
using CleanArchitecture;

namespace Application.UseCases.Services.DeleteService
{

    public interface IDeleteServiceOutputPort : IOutputPort
    {

        #region Methods

        Task PresentDeletedServiceAsync(ServiceDto service, CancellationToken cancellationToken);

        Task PresentServiceInUseAsync(CancellationToken cancellationToken);

        Task PresentServiceNotFoundAsync(Guid serviceID, CancellationToken cancellationToken);

        Task PresentUnauthorisedAsync(string message, CancellationToken cancellationToken);

        #endregion

    }

}
