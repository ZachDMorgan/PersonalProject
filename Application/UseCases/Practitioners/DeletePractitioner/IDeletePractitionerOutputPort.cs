using Application.Dtos;
using CleanArchitecture;

namespace Application.UseCases.Practitioners.DeletePractitioner
{

    public interface IDeletePractitionerOutputPort : IOutputPort
    {

        #region Methods

        Task PresentDeletedPractitionerAsync(PractitionerDto Practitioner, CancellationToken cancellationToken);

        Task PresentPractitionerInUseAsync(CancellationToken cancellationToken);

        Task PresentPractitionerNotFoundAsync(Guid PractitionerID, CancellationToken cancellationToken);

        Task PresentUnauthorisedAsync(string message, CancellationToken cancellationToken);

        #endregion

    }

}
