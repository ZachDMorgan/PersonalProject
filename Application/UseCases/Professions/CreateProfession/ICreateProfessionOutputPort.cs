using Application.Dtos;
using Application.Services.Validation;
using CleanArchitecture;

namespace Application.UseCases.Professions.CreateProfession
{

    public interface ICreateProfessionOutputPort : IOutputPort
    {

        #region Methods

        Task PresentInvalidInputAsync(ICollection<ValidationError> validationErrors, CancellationToken cancellationToken);

        Task PresentProfessionAsync(ProfessionDto profession, CancellationToken cancellationToken);

        Task PresentProfessionNameNotUniqueAsync(CancellationToken cancellationToken);

        Task PresentUnauthorisedAsync(string message, CancellationToken cancellationToken);

        #endregion

    }

}
