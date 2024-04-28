using Application.Dtos;
using CleanArchitecture;

namespace Application.UseCases.Practices.DeletePractice
{

    public interface IDeletePracticeOutputPort : IOutputPort
    {

        #region Methods

        Task PresentDeletedPracticeAsync(PracticeDto Practice, CancellationToken cancellationToken);

        Task PresentPracticeNotFoundAsync(Guid PracticeID, CancellationToken cancellationToken);

        Task PresentUnauthorisedAsync(string message, CancellationToken cancellationToken);

        #endregion

    }

}
