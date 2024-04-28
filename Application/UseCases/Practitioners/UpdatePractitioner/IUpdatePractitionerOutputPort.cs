using Application.Dtos;
using Application.Services.Validation;
using CleanArchitecture;

namespace Application.UseCases.Practitioners.UpdatePractitioner
{

    public interface IUpdatePractitionerOutputPort : IOutputPort
    {

        #region Methods

        Task PresentInvalidInputAsync(ICollection<ValidationError> validationErrors, CancellationToken cancellationToken);

        Task PresentPractitionerAsync(PractitionerDto Practitioner, CancellationToken cancellationToken);

        Task PresentPractitionerNotFoundAsync(Guid PractitionerID, CancellationToken cancellationToken);

        Task PresentProfessionNotFoundAsync(Guid professionID, CancellationToken cancellationToken);

        Task PresentServicesNotFoundAsync(ICollection<Guid> invalidServiceIDs, CancellationToken cancellationToken);

        Task PresentUnauthorisedAsync(string message, CancellationToken cancellationToken);

        #endregion

    }

}
