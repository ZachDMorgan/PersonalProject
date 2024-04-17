using Application.Dtos;
using Application.Services.Validation;
using CleanArchitecture;

namespace Application.UseCases.Practices.CreatePractice
{

    public interface ICreatePracticeOutputPort : IOutputPort
    {

        #region Methods

        Task PresentInvalidInputAsync(ICollection<ValidationError> validationErrors, CancellationToken cancellationToken);

        Task PresentPracticeAsync(PracticeDto practice, CancellationToken cancellationToken);

        Task PresentPracticeNameNotUniqueAsync(CancellationToken cancellationToken);

        Task PresentUnauthorisedAsync(string message, CancellationToken cancellationToken);

        #endregion

    }

}
