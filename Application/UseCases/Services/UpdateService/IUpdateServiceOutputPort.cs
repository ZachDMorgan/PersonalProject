using Application.Dtos;
using Application.Services.Validation;
using CleanArchitecture;

namespace Application.UseCases.Services.UpdateService
{

    public interface IUpdateServiceOutputPort : IOutputPort
    {

        #region Methods

        Task PresentInvalidInputAsync(ICollection<ValidationError> validationErrors, CancellationToken cancellationToken);

        Task PresentServiceAsync(ServiceDto service, CancellationToken cancellationToken);

        Task PresentServiceNameNotUniqueAsync(CancellationToken cancellationToken);

        Task PresentServiceNotFoundAsync(Guid serviceID, CancellationToken cancellationToken);

        Task PresentUnauthorisedAsync(string message, CancellationToken cancellationToken);

        #endregion

    }

}
