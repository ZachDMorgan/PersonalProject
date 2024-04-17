using Application.Dtos;
using Application.Services.Validation;
using CleanArchitecture;

namespace Application.UseCases.Practitioners.CreatePractitioner
{

    public interface ICreatePractitionerOutputPort : IOutputPort
    {

        #region Methods

        Task PresentInvalidInputAsync(ICollection<ValidationError> validationErrors, CancellationToken cancellationToken);

        Task PresentPractitionerAsync(PractitionerDto practice, CancellationToken cancellationToken);

        Task<ContinuationResult> PresentPractitionerNameNotUniqueAsync(CancellationToken cancellationToken);

        Task PresentProfessionDoesNotExistAsync(Guid invalidID, CancellationToken cancellationToken);

        Task PresentServicesDoNotExistAsync(IEnumerable<Guid> invalidIDs, CancellationToken cancellationToken);

        Task PresentUnauthorisedAsync(string message, CancellationToken cancellationToken);

        #endregion

    }

}
