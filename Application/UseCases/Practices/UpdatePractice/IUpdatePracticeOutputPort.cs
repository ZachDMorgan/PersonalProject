using Application.Dtos;
using Application.Services.Validation;
using CleanArchitecture;

namespace Application.UseCases.Practices.UpdatePractice
{

    public interface IUpdatePracticeOutputPort : IOutputPort
    {

        #region Methods

        Task PresentInvalidInputAsync(ICollection<ValidationError> validationErrors, CancellationToken cancellationToken);

        Task PresentPracticeAsync(PracticeDto Practice, CancellationToken cancellationToken);

        Task PresentPracticeNameNotUniqueAsync(CancellationToken cancellationToken);

        Task PresentPracticeNotFoundAsync(Guid PracticeID, CancellationToken cancellationToken);

        Task PresentUnauthorisedAsync(string message, CancellationToken cancellationToken);

        #endregion

    }

}
