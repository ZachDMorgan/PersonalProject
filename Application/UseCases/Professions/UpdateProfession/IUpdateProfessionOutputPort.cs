using Application.Dtos;
using Application.Services.Validation;
using CleanArchitecture;

namespace Application.UseCases.Professions.UpdateProfession
{

    public interface IUpdateProfessionOutputPort : IOutputPort
    {

        #region Methods

        Task PresentInvalidInputAsync(ICollection<ValidationError> validationErrors, CancellationToken cancellationToken);

        Task PresentProfessionAsync(ProfessionDto Profession, CancellationToken cancellationToken);

        Task PresentProfessionNameNotUniqueAsync(CancellationToken cancellationToken);

        Task PresentProfessionNotFoundAsync(Guid ProfessionID, CancellationToken cancellationToken);

        Task PresentUnauthorisedAsync(string message, CancellationToken cancellationToken);

        #endregion

    }

}
